using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingBehaviour : MonoBehaviour
{
    private float mSize = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.Q))
        {
            mSize += 0.1f;
            if (mSize >=100)
            {
                mSize = 100;
            }
        }
        else
        {
            mSize -= 0.1f;
            if (mSize <=0)
            {
                mSize = 0;
            }
        }
        SetBlend(mSize);
    }
    void SetBlend(float val)
    {
        //update blendshape
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, val);
    }
}
