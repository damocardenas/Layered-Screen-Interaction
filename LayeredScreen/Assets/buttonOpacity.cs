using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buttonOpacity : MonoBehaviour
{
    public Material[] materials;
    public Color32[] colors;
    public bool on = true;
    public bool perma_off = false;

    public bool header_guy = false;

    public void flip_active()
    {
        if (header_guy)
        {
            if (!on)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(4).gameObject.SetActive(true);
                on = true;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(false);
                on = false;
            }
        }
        else
        {

            if (!perma_off)
            {
                if (!on)
                {
                    transform.GetChild(4).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[0];
                    transform.GetChild(5).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[2];
                    transform.GetChild(1).gameObject.SetActive(true);
                    //transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = colors[0];
                    GetComponent<BoxCollider>().enabled = true;
                    GetComponent<NearInteractionTouchable>().enabled = true;
                    on = true;
                }
                else
                {
                    GetComponent<menu_guy>().turn_menu_off();
                    transform.GetChild(4).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[1];
                    transform.GetChild(5).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[1];
                    transform.GetChild(1).gameObject.SetActive(false);
                    //transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = colors[1];
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<NearInteractionTouchable>().enabled = false;
                    on = false;
                }
            }
        }
    }

    public void deactivate()
    {
        flip_active();
        perma_off = true;
    }

    public void reactivate()
    {
        perma_off = false;
        flip_active();
    }
}
