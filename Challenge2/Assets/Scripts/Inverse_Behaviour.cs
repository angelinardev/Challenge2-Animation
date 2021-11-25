//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



//[RequireComponent(typeof(Animator))]
//public class Inverse_Behaviour : MonoBehaviour
//{

//    public Transform targetTransform;
//    public Transform aimTransform;
//    public Transform bone;
//    public int iterations = 10;
//    public bool Active = false;
//    [Range(0,1)]
//    float weight = 1.0f;

//    public HumanBone[] humanBones;
//    Transform[] boneTransforms;

//    Animator anim;
//    // Start is called before the first frame update
//    void Start()
//    {
//        Animator animator = GetComponent<Animator>();
//        boneTransforms = new Transform[humanBones.Length];
//        for(int i = 0; i<boneTransforms.Length; i++)
//        {
//            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
//        }
//    }

//    // Update is called once per frame
//    void LateUpdate()
//    {
//        Vector3 targetPosition = targetTransform.position;
//        AimAtTarget(bone, targetPosition);
//    }

//    private void AimAtTarget(Transform bone, Vector3 targetPosition)
//    {
//        Vector3 aimDirection = aimTransform.forward;
//        Vector3 targetDirection = targetPosition - aimTransform.position;
//        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
//        bone.rotation = aimTowards * bone.rotation;
//    }

//    //private void OnAnimatorIK(int layerIndex)
//    //{
//    //    if (Active) {
//    //        // Set the look target position, if one has been assigned
//    //        if (target != null)
//    //        {
//    //            anim.SetLookAtWeight(1);
//    //            anim.SetLookAtPosition(target.position);

//    //            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
//    //            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

//    //            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
//    //            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

//    //            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
//    //            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

//    //            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
//    //            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
//    //        }

//    //        // Set the right hand target position and rotation, if one has been assigned
           
              


//    //    //if the IK is not active, set the position and rotation of the hand and head back to the original position
//    //    else
//    //    {
//    //        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
//    //        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

//    //        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
//    //        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

//    //        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
//    //        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);

//    //        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
//    //        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);

//    //        anim.SetLookAtWeight(0);
//    //    }

//    //}
//    //}
//}
