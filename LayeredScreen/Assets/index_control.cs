using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class index_control : MonoBehaviour
{
    [SerializeField]
    private bool right_handed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Handedness hand;
        if (right_handed) hand = Handedness.Right;
        else hand = Handedness.Left;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, hand, out MixedRealityPose pose))
        {
            transform.position = pose.Position;
        }
    }
}
