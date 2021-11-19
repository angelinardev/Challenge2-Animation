using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public enum InterpMode
    {
        lerp = 0,
        catmull = 1,
        bezier = 2
    }

    public List<Node> nodes;
    public InterpMode mode;

    private float _segmentTimer = 0;
    public float _segmentTravelTime = 1.0f;
    private int _segmentIndex = 0;

    // TODO: Create animation curve and add keys to use as motion graph
    private AnimationCurve _curve;

    private void Start()
    {
        nodes = new List<Node>(FindObjectsOfType<Node>());
        _curve = new AnimationCurve();

        //motion graph
        //slow and then speed up
        _curve.AddKey(0.0f, 0.0f);
        _curve.AddKey(0.15f, 0.1f);
        _curve.AddKey(0.3f, 0.15f);
        _curve.AddKey(0.4f, 0.2f);
        _curve.AddKey(0.75f, 0.5f);
        _curve.AddKey(0.9f, 0.8f);
        _curve.AddKey(0.95f, 0.9f);
        _curve.AddKey(1.0f, 1.0f);
    }

  
    private void Update()
    {
        switch (mode)
        {
            case InterpMode.lerp:
                {
                    _segmentTimer += Time.deltaTime;

                    if (_segmentTimer > _segmentTravelTime)
                    {
                        _segmentTimer = 0f;
                        _segmentIndex += 1;

                        if (_segmentIndex >= nodes.Count)
                            _segmentIndex = 0;
                    }

                    float t = _segmentTimer / _segmentTravelTime;

                    if (nodes.Count < 2)
                    {
                        transform.position = Vector3.zero;
                        return;
                    }

                    Vector3 p0, p1;
                    int p0_index, p1_index;

                    p1_index = _segmentIndex;
                    p0_index = (p1_index == 0) ? nodes.Count - 1 : p1_index - 1;

                    p0 = nodes[p0_index].transform.position;
                    p1 = nodes[p1_index].transform.position;

                    transform.position = LERP(p0, p1, t);
                }
                break;

            case InterpMode.catmull:
                {
                    _segmentTimer += Time.deltaTime;

                    if (_segmentTimer > _segmentTravelTime)
                    {
                        _segmentTimer = 0f;
                        _segmentIndex += 1;

                        if (_segmentIndex >= nodes.Count)
                            _segmentIndex = 0;
                    }

                    float t = _segmentTimer / _segmentTravelTime;

                    if (nodes.Count < 4)
                    {
                        transform.position = Vector3.zero;
                        return;
                    }

                    Vector3 p0, p1, p2, p3;
                    int p0_index, p1_index, p2_index, p3_index;

                    p1_index = _segmentIndex;
                    p0_index = (p1_index == 0) ? nodes.Count - 1 : p1_index - 1;
                    p2_index = (p1_index + 1) % nodes.Count;
                    p3_index = (p2_index + 1) % nodes.Count;

                    p0 = nodes[p0_index].transform.position;
                    p1 = nodes[p1_index].transform.position;
                    p2 = nodes[p2_index].transform.position;
                    p3 = nodes[p3_index].transform.position;

                    transform.position = Catmull(p0, p1, p2, p3, _curve.Evaluate(t));

                    //
                    
                }
                break;

            case InterpMode.bezier:
                {
                    _segmentTimer += Time.deltaTime;

                    if (_segmentTimer > _segmentTravelTime)
                    {
                        _segmentTimer = 0f;
                        _segmentIndex += 1;

                        if (_segmentIndex >= 0)
                            _segmentIndex = 0;
                    }

                    float t = _segmentTimer / _segmentTravelTime;

                    if (nodes.Count < 4)
                    {
                        transform.position = Vector3.zero;
                        return;
                    }

                    Vector3 p0, p1, p2, p3;
                    int p0_index, p1_index, p2_index, p3_index;

                    p0_index = _segmentIndex;
                    p1_index = (p0_index + 1);
                    p2_index = (p1_index + 1);
                    p3_index = (p2_index + 1);

                    p0 = nodes[p0_index].transform.position;
                    p1 = nodes[p1_index].transform.position;
                    p2 = nodes[p2_index].transform.position;
                    p3 = nodes[p3_index].transform.position;

                    transform.position = Bezier(p0, p1, p2, p3, t);
                }
                break;

            default:
                break;
        }
    }

    public Vector3 LERP(Vector3 p0, Vector3 p1, float t)
    {
        return (1.0f - t) * p0 + t * p1;
    }

    public Vector3 Catmull(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (2.0f * p1 + t * (-p0 + p2)
        + t * t * (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3)
        + t * t * t * (-p0 + 3.0f * p1 - 3.0f * p2 + p3));
    }

    public Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return ((p0) + t * (3.0f * -p0 + 3.0f * p1)
            + t * t * (3.0f * p0 - 6.0f * p1 + 3.0f * p2)
            + t * t * t * (-p0 + 3.0f * p1 - 3.0f * p2 + p3));
    }
}