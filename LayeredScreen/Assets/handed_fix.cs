using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handed_fix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Trial ordering").GetComponent<trial_guy>().is_right_handed) transform.position = new Vector3( -transform.position.x - 0.06f, transform.position.y, transform.position.z );

    }
}
