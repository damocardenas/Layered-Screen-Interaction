using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeDetector : MonoBehaviour
{
    [SerializeField]
    private bool ready;
    [SerializeField, Range(0, 2)]
    private int detector_identifier = -1;
    [SerializeField]
    private GameObject active_detector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_active(GameObject active)
    {
        active_detector = active;
        ready = true;
    }

    public void set_ready(bool r)
    {
        ready = r;
    }

    public void gaze_detected()
    {
        if (ready)
        {
            if (detector_identifier > 0)
            {
                ready = false;
                if (detector_identifier == 1) GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().cycle_backwards();
                else GameObject.Find("MixedRealitySceneContent").GetComponent<screenManager>().cycle_forwards();
                GameObject.Find("reset_cube").GetComponent<eyeDetector>().set_active(gameObject);
            }
            else
            {
                ready = false;
                active_detector.GetComponent<eyeDetector>().set_ready(true);
            }
        }
    }
}
