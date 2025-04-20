using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Microsoft.MixedReality.GraphicsTools.MeshInstancer;

public class handTracking : MonoBehaviour
{
    [SerializeField]
    private float distance_increments;
    [SerializeField]
    private float distance_offset;
    public GameObject headset_go;
    int current_screen = 1;
    int origin_screen = 1;
    GameObject manager;
    [SerializeField]
    Material[] materials;
    [SerializeField]
    private bool right_handed;
    [SerializeField]
    private float gesture_threshold;
    bool gest_detected = false;
    bool gest_start = true;

    Vector3 start_pos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        right_handed = GameObject.Find("Trial ordering").GetComponent<trial_guy>().is_right_handed;
    }

    // Update is called once per frame
    void Update()
    {
        new_method();
    }

    public void set_hand(bool right)
    {
        right_handed = right;
    }

    void new_method()
    {
        Handedness hand;
        if (right_handed) hand = Handedness.Right;
        else hand = Handedness.Left;

        gest_detected = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, hand, out MixedRealityPose pose2))
        {
            //transform.GetChild(1).gameObject.transform.position = pose2.Position;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, hand, out MixedRealityPose pose3))
            {
                //transform.GetChild(2).gameObject.transform.position = pose3.Position;
                float finger_dist = (pose2.Position - pose3.Position).magnitude;

                // if gesture detected
                if (finger_dist < gesture_threshold)
                {

                    //transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = materials[0];
                    if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, hand, out MixedRealityPose pose))
                    {

                        gest_detected = true;
                        if (gest_start)
                        {
                            origin_screen = current_screen;
                            start_pos = pose.Position;
                            gest_start = false;
                        }

                        //transform.GetChild(0).gameObject.transform.position = pose.Position;



                        float start_hand_distance = (new Vector3(pose.Position.x, 0f, pose.Position.z) - new Vector3(start_pos.x, 0f, start_pos.z)).magnitude;
                        float hand_body_distance = (new Vector3(pose.Position.x, 0f, pose.Position.z) - new Vector3(headset_go.transform.position.x, 0f, headset_go.transform.position.z)).magnitude;
                        float start_body_distance = (new Vector3(start_pos.x, 0f, start_pos.z) - new Vector3(headset_go.transform.position.x, 0f, headset_go.transform.position.z)).magnitude;

                        if (origin_screen == 1)
                        {
                            if (start_hand_distance > distance_increments * 2 && start_body_distance < hand_body_distance)
                            {
                                if (current_screen != 3)
                                {
                                    current_screen = 3;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else if (start_hand_distance > distance_increments && start_body_distance < hand_body_distance)
                            {
                                if (current_screen != 2)
                                {
                                    current_screen = 2;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else
                            {
                                if (current_screen != 1)
                                {
                                    current_screen = 1;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                        }
                        else if (origin_screen == 2)
                        {
                            if (start_hand_distance > distance_increments && start_body_distance < hand_body_distance)
                            {
                                if (current_screen != 3)
                                {
                                    current_screen = 3;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else if (start_hand_distance > distance_increments && start_body_distance > hand_body_distance)
                            {
                                if (current_screen != 1)
                                {
                                    current_screen = 1;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else
                            {
                                if (current_screen != 2)
                                {
                                    current_screen = 2;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                        }
                        else if (origin_screen == 3)
                        {
                            if (start_hand_distance > distance_increments*2 && start_body_distance > hand_body_distance)
                            {
                                if (current_screen != 1)
                                {
                                    current_screen = 1;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else if (start_hand_distance > distance_increments && start_body_distance > hand_body_distance)
                            {
                                if (current_screen != 2)
                                {
                                    current_screen = 2;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else
                            {
                                if (current_screen != 3)
                                {
                                    current_screen = 3;
                                    //transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = materials[1];
                    gest_start = true;
                }
            }

        }

        /*void old_method()
        {
            Handedness hand;
            if (right_handed) hand = Handedness.Right;
            else hand = Handedness.Left;


            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, hand, out MixedRealityPose pose2))
            {
                transform.GetChild(1).gameObject.transform.position = pose2.Position;
                if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, hand, out MixedRealityPose pose3))
                {
                    transform.GetChild(2).gameObject.transform.position = pose3.Position;
                    float finger_dist = (pose2.Position - pose3.Position).magnitude;
                    if (finger_dist < gesture_threshold)
                    {
                        transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = materials[0];
                        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, hand, out MixedRealityPose pose))
                        {
                            transform.GetChild(0).gameObject.transform.position = pose.Position;
                            float distance = (new Vector3(pose.Position.x, 0f, pose.Position.z) - new Vector3(headset_go.transform.position.x, 0f, headset_go.transform.position.z)).magnitude;
                            if (distance > distance_increments * 2 + distance_offset)
                            {
                                if (current_screen != 3)
                                {
                                    current_screen = 3;
                                    transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else if (distance > distance_increments + distance_offset)
                            {
                                if (current_screen != 2)
                                {
                                    current_screen = 2;
                                    transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                            else
                            {
                                if (current_screen != 1)
                                {
                                    current_screen = 1;
                                    transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[current_screen - 1];
                                    GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().Set_screen(current_screen);
                                }
                            }
                        }
                    }
                    else
                    {
                        transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = materials[1];
                    }
                }
            }
        }*/
    }
}
