using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class screenOpacity : MonoBehaviour
{
    GameObject enabled_guy;
    bool on = false;
    public bool is_email_guy;
    public int my_id;

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

    public void other_menus_off()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (i != my_id) transform.parent.GetChild(i).gameObject.GetComponent<screenOpacity>().menu_off();
        }
    }

    public void menu_off()
    {
        if (on)
        {
            enabled_guy.GetComponent<menu_guy>().turn_menu_off();
            on = false;
        }
    }

    public void set_enabled(GameObject enabled_soon)
    {
        other_menus_off();
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
