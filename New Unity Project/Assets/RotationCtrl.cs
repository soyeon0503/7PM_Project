using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class RotationCtrl : MonoBehaviour
{
    Transform parentObj;
    Vector3 startPos;
    Vector3 originalPos;

    void Start()
    {
        startPos = transform.localPosition;
        originalPos = transform.localPosition;
        parentObj = transform.root;
 
    }

    void Update()
    {
        ResetVR();
    }

    void ResetVR()
    {
        if (parentObj != null)
        {
            startPos -= InputTracking.GetLocalPosition(XRNode.CenterEye);

            Quaternion tempRot = Quaternion.Inverse(parentObj.localRotation);
            Vector3 newAngle = tempRot.eulerAngles;
            transform.localEulerAngles = new Vector3(35,originalPos.y, originalPos.z);
        }
    }

}
