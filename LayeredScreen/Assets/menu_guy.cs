using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_guy : MonoBehaviour
{
    bool currently_enabled = false;

    public void flip_active()
    {
        Debug.Log(currently_enabled);
        if (currently_enabled)
        {
            transform.GetChild(6).gameObject.SetActive(false);
            currently_enabled = false;
            transform.parent.gameObject.GetComponent<screenOpacity>().is_off();
        }
        else
        {
            transform.GetChild(6).gameObject.SetActive(true);
            currently_enabled = true;
            transform.parent.gameObject.GetComponent<screenOpacity>().set_enabled(gameObject);
        }
    }

    public void regen()
    {
        transform.GetChild(6).gameObject.GetComponent<randomize_slots>().regenerate();
    }

    public void turn_menu_off()
    {
        Debug.Log("Turn off");
        transform.GetChild(6).gameObject.SetActive(false);
        currently_enabled = false;
    }
}
