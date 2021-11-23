using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    public GameObject target; // Assign this in the inspector

    private Vector3 _movementVector = new Vector3(1, 1, 1);
    public float _maxVelocity = 10.0f;
    private float _desiredVelocity;

    public float slowingRadius = 10.0f;

    private Vector3 direction = new Vector3(1, 1, 1);
    private bool hitObj = false;

    private Vector3 initialDirection;

    private bool arrive, avoid, flee = false;
    private bool pressed1, pressed2, pressed3 = false;

    private Rigidbody body;

    private void Start()
    {
        initialDirection = (target.transform.position - transform.position);
        body = GetComponent<Rigidbody>();
    }

    private void Controls()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!pressed1)
            {
                //slowingRadius = 1.0f;
                arrive = true;
                print("activated arrival behaviour\n");
            }
            else
            {
                arrive = false;
                print("deactivated arrival behaviour\n");
            }
            pressed1 = !pressed1;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!pressed2)
            {
                //slowingRadius = 1.0f;
                avoid = true;
                print("activated avoid behaviour\n");
            }
            else
            {
                avoid = false;
                print("deactivated avoid behaviour\n");
            }
            pressed2 = !pressed2;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!pressed3)
            {
                flee = true;
                print("activated flee behaviour\n");
            }
            else
            {
                flee = false;
                print("deactivated flee behaviour\n");
            }
            pressed3 = !pressed3;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        Controls();
        
        //change behaviour depending on key press
        if (arrive)
        {
            ArrivalBehaviour();
        }
        if (avoid)
        {
            AvoidCollisionBehaviour();
        }
        if (flee)
        {
            FleeBehaviour();
        }

       _movementVector = _movementVector * _desiredVelocity * Time.deltaTime;
        

        //Debug.Log(_movementVector);
        //Debug.Log(_desiredVelocity);

        transform.position += _movementVector*2.0f;
        //body.velocity.Set(_movementVector.x, _movementVector.y, _movementVector.z);
    }

    private void ArrivalBehaviour()
    {
        // TODO: update movementvector each frame using this function to achieve desired behaviour
        float distance = (target.transform.position - transform.position).magnitude;
        direction = (target.transform.position - transform.position);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        //check the distance to see if we're in the slowing area
        RaycastHit[] hitAll = Physics.RaycastAll(transform.position, -transform.position + target.transform.position, slowingRadius);
        for (int i = 0; i < hitAll.Length; i++)
        {
            if (hitAll[i].collider != null && hitAll[i].transform.gameObject != gameObject)
            {
                //inside
                _desiredVelocity = _desiredVelocity * _maxVelocity * (distance * Time.deltaTime / slowingRadius);
                hitObj = true;
                Debug.Log("hit");

                //segment travel time reduce
                transform.GetComponent<SpeedController>()._segmentTravelTime = 3.8f;

            }
        }
        if (!hitObj)
        {
            //outside
            _desiredVelocity = _desiredVelocity * _maxVelocity;

            transform.GetComponent<SpeedController>()._segmentTravelTime = 1.0f;
        }

        _movementVector = (target.transform.position - transform.position).normalized;

        hitObj = false;

    }

    private void FleeBehaviour()
    {
        // TODO: update movementvector each frame using this function to achieve desired behaviour
        float distance = (target.transform.position - transform.position).magnitude; 
        //Debug.Log(distance);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        
        //quicker the closer we are to the target
        _desiredVelocity = _desiredVelocity * _maxVelocity * (slowingRadius / distance);

        _desiredVelocity *= -1; //flip direction

        _movementVector = (target.transform.position - transform.position).normalized;
    }

    private void AvoidCollisionBehaviour()
    {
        hitObj = false;
        // TODO: update movementvector each frame using this function to achieve desired behaviour
        direction = (target.transform.position - transform.position);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        //Debug.Log(direction);
        //check the distance to see if we're in the slowing area
        RaycastHit[] hitAll = Physics.RaycastAll(transform.position, direction, slowingRadius);
        for (int i = 0; i < hitAll.Length; i++)
        {
            if (hitAll[i].collider != null && hitAll[i].transform.gameObject != gameObject)
            {
                //inside
                direction = new Vector3(target.transform.position.x * -1, target.transform.position.y*-1, target.transform.position.z * -1);
                    
               // Rotate the forward vector towards the target direction by one step
              Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _desiredVelocity * Time.deltaTime, 1.0f);

              // Calculate a rotation a step closer to the target and applies rotation to this object
               transform.rotation = Quaternion.LookRotation(newDirection);

                hitObj = true;
                Debug.Log("hit");

            }  
        }
       _desiredVelocity = _desiredVelocity *direction.normalized.magnitude* _maxVelocity;

        _movementVector = direction.normalized;
        if (hitObj)
        {
            _movementVector = new Vector3((target.transform.position.x - transform.position.x) * -1, (target.transform.position.y - transform.position.y)*-1, (target.transform.position.z - transform.position.z) * -1).normalized;
        }
        else
        {
            _movementVector = direction.normalized;
        }
        if ((target.transform.position - transform.position).magnitude > slowingRadius-0.5)
        {
            _movementVector = initialDirection.normalized;
            //transform.rotation = Quaternion.LookRotation(_movementVector);

        }

        hitObj = false;
    }
   
   
}