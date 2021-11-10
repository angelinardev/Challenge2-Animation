using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    public GameObject target; // Assign this in the inspector

    private Vector3 _movementVector = new Vector3(1, 0, 1);
    private float _maxVelocity = 10.0f;
    private float _desiredVelocity;

    private float slowingRadius = 2.0f;

    private Vector3 direction = new Vector3(1, 0, 1);
    private bool hitObj = false;

    private Vector3 initialDirection;


    private void Start()
    {
        initialDirection = (target.transform.position - transform.position);
    }

    //private Vector3 steering;

    // Update is called once per frame
    private void Update()
    {
        // TODO: call steering behaviour function here

        //ArrivalBehaviour();
        AvoidCollisionBehaviour();
        //FleeBehaviour();

       _movementVector = _movementVector * _desiredVelocity * Time.deltaTime;
        

        //Debug.Log(_movementVector);
        //Debug.Log(_desiredVelocity);

        transform.position += _movementVector;
    }

    private void ArrivalBehaviour()
    {
        // TODO: update movementvector each frame using this function to achieve desired behaviour
        float distance = (target.transform.position - transform.position).magnitude;
        //Debug.Log(distance);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        //check the distance to see if we're in the slowing area
        if (distance < slowingRadius)
        {
            //inside
            _desiredVelocity = _desiredVelocity * _maxVelocity * (distance * Time.deltaTime / slowingRadius);
        }
        
        else
        {
            //outside
            _desiredVelocity = _desiredVelocity * _maxVelocity;
        }

        _movementVector = (target.transform.position - transform.position).normalized;
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
        // TODO: update movementvector each frame using this function to achieve desired behaviour
        direction = (target.transform.position - transform.position);
        _desiredVelocity = (target.transform.position - transform.position).normalized.magnitude;
        //check the distance to see if we're in the slowing area
        RaycastHit[] hitAll = Physics.RaycastAll(transform.position, -transform.position+target.transform.position, slowingRadius);
        for (int i = 0; i < hitAll.Length; i++)
        {
            if (hitAll[i].collider != null && hitAll[i].transform.gameObject != gameObject)
            {
                //inside
                direction = new Vector3(target.transform.position.x * -1, 0, target.transform.position.z * -1);
                    
               // Rotate the forward vector towards the target direction by one step
              Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _desiredVelocity * Time.deltaTime, 1.0f);

              // Calculate a rotation a step closer to the target and applies rotation to this object
               transform.rotation = Quaternion.LookRotation(newDirection);

                hitObj = true;

            }  
        }
       _desiredVelocity = _desiredVelocity *direction.normalized.magnitude* _maxVelocity;

        _movementVector = direction.normalized;
        if (hitObj)
        {
            _movementVector = new Vector3((target.transform.position.x - transform.position.x) * -1, 0, target.transform.position.z - transform.position.z * -1).normalized;
        }
        if ((target.transform.position - transform.position).magnitude > slowingRadius + 5.0f)
        {
           _movementVector = initialDirection.normalized;
        }
    }
}