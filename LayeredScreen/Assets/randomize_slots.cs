using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize_slots : MonoBehaviour
{
    public Material[] materials;
    bool[] availability = new bool[8];

    private void Start()
    {
        //random_generation();
    }

    public void random_generation()
    {
        Debug.Log("GENERATING");
        int i = 0;
        foreach (MeshRenderer child_renderer in transform.GetChild(1).GetComponentsInChildren<MeshRenderer>())
        {
            if (Random.Range(0, 2) == 0)
            {
                child_renderer.material = materials[0];
                availability[i] = true;
            }
            else
            {
                child_renderer.material = materials[1];
                availability[i] = false;
            }
            i++;
        }
    }

    public void regenerate()
    {
        Debug.Log("REGENERATING");
        random_generation();
    }

    public bool get_av(int hour)
    {
        return availability[hour-1];
    }
}
