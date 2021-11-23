using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingBehaviour : MonoBehaviour
{
    private float mSize = 0;
    private float mSize2 = 0;
    int blendShapeCount;
   Mesh skinnedMesh;
    bool Finished = false;
    bool Finished2 = false;
    SkinnedMeshRenderer skinnedMeshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        blendShapeCount = skinnedMesh.blendShapeCount;

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

        if (Input.GetKey(KeyCode.R))
        {
            mSize2 += 0.1f;
            if (mSize2 >= 100)
            {
                mSize2 = 100;
            }
        }
        else
        {
            mSize2 -= 0.1f;
            if (mSize2 <= 0)
            {
                mSize2 = 0;
            }
        }


        SetBlend(mSize);
        SetBlend2(mSize2);
    }
    void SetBlend(float val)
    {
        if (Finished2 && val < 100f)
        {
            //update blendshape

            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, val);
        }
            
            if(val == 0)
        {
            Finished = true;
        }
        else
        {
            Finished = false;
        }
    }

    void SetBlend2(float val)
    {
        if (Finished && val < 100f)
        {
            //update blendshape
            skinnedMeshRenderer.SetBlendShapeWeight(1, val);
        }

        if(val == 0)
        {
            Finished2 = true;
        }
        else
        {
            Finished2 = false;
        }
    }
}
