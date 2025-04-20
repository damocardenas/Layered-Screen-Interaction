using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix_pos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject headset = GameObject.Find("Trial ordering");
        transform.position = transform.position + headset.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
