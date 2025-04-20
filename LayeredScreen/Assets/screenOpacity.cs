using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class screenOpacity : MonoBehaviour
{
    GameObject enabled_guy;
    bool on = false;
    public bool is_email_guy;

    public void flip_screen_enabled()
    {
        on = false;
        Debug.Log("Flipping activity on screen: " + gameObject.name);
        if (is_email_guy)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<buttonOpacity>().flip_active();
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<buttonOpacity>().flip_active();
            }
        }
    }

    public void regen_all()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<menu_guy>().regen();
        }
    }

    public void set_enabled(GameObject enabled_soon)
    {
        if (on)
        {
            enabled_guy.GetComponent<menu_guy>().turn_menu_off();
        }
        enabled_guy = enabled_soon;
        on = true;
    }

    public void is_off()
    {
        on = false;
    }
}
