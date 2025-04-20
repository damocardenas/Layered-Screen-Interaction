using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenManager : MonoBehaviour
{
    int active_screen = 1;
    int screen_swaps = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.GetComponent<screenOpacity>().flip_screen_enabled();
        transform.GetChild(2).gameObject.GetComponent<screenOpacity>().flip_screen_enabled();
    }


    public void Set_screen(int screen)
    {
        if (active_screen != screen) screen_swaps++;
        Debug.Log("Enabling screen: " + screen);
        transform.GetChild(screen - 1).gameObject.GetComponent<screenOpacity>().flip_screen_enabled();
        Debug.Log("Disabling screen: " + active_screen);
        transform.GetChild(active_screen - 1).gameObject.GetComponent<screenOpacity>().flip_screen_enabled();
        active_screen = screen;
    }

    public void cycle_forwards()
    {
        if (active_screen != 1)
        {
            Set_screen(active_screen - 1);
        }
    }

    public void cycle_backwards()
    {
        if (active_screen != 3)
        {
            Set_screen(active_screen + 1);
        }
    }

    public int get_swaps()
    {
        return screen_swaps;
    }

    public void set_multi_screen()
    {
        transform.GetChild(1).Rotate(new Vector3(0f, -25f, 0f));
        transform.GetChild(2).Rotate(new Vector3(0f, 25f, 0f));
        transform.GetChild(1).position = transform.GetChild(0).position + new Vector3(-0.1f, 0f, 0f);
        transform.GetChild(2).position = transform.GetChild(0).position + new Vector3(0.1f, 0f, 0f);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != active_screen - 1) transform.GetChild(i).GetComponent<screenOpacity>().flip_screen_enabled();
        }
    }

    public void unset_multi_screen()
    {
        transform.GetChild(1).Rotate(new Vector3(0f, 25f, 0f));
        transform.GetChild(2).Rotate(new Vector3(0f, -25f, 0f));
        transform.GetChild(1).position = transform.GetChild(0).position + new Vector3(0f, 0f, 0.05f);
        transform.GetChild(2).position = transform.GetChild(0).position + new Vector3(0f, 0f, 0.1f);

        transform.GetChild(1).gameObject.GetComponent<screenOpacity>().flip_screen_enabled();
        transform.GetChild(2).gameObject.GetComponent<screenOpacity>().flip_screen_enabled();
        active_screen = 1;
    }
}
