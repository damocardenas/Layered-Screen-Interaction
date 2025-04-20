using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionMethodController : MonoBehaviour
{
    /*
     * Interaction method index:
     *  0 - button controls (replace with all on/all off)
     *  1 - eye tracking
     */
    [SerializeField]
    private int currentInteractionMethod;
    private Action[,] interactableFunctionCalls = new Action[4,2];
    [SerializeField]
    private GameObject eye_tracking;
    [SerializeField]
    private GameObject button_controls;
    [SerializeField]
    private GameObject hand_controls;
    [SerializeField]
    private GameObject multi_screen_go;

    private void Start()
    {
        interactableFunctionCalls[0,0] = () => disable_eye_tracking();
        interactableFunctionCalls[0, 1] = () => enable_eye_tracking();
        interactableFunctionCalls[1, 0] = () => disable_button_controls();
        interactableFunctionCalls[1, 1] = () => enable_button_controls();
        interactableFunctionCalls[2, 0] = () => disable_hand_controls();
        interactableFunctionCalls[2, 1] = () => enable_hand_control();
        interactableFunctionCalls[3, 0] = () => disable_multi_screen();
        interactableFunctionCalls[3, 1] = () => enable_multi_screen();
        update_output();
    }

    public void change_method(int new_method)
    {
        interactableFunctionCalls[currentInteractionMethod, 0]();
        interactableFunctionCalls[new_method, 1]();
        currentInteractionMethod = new_method;
        update_output();
    }

    public void disable_last_method()
    {
        interactableFunctionCalls[currentInteractionMethod, 0]();
    }

    public void enable_next_method(int new_method)
    {
        interactableFunctionCalls[new_method, 1]();
        currentInteractionMethod = new_method;
        update_output();
    }

    void update_output()
    {
        string int_method = "";
        switch (currentInteractionMethod)
        {
            case 0:
                int_method = "Buttons";
                break;
            case 1:
                int_method = "Eye Tracking";
                break;
            case 2:
                int_method = "Hand Slider";
                break;
            case 3:
                int_method = "Multi Screen";
                break;
            default:
                Debug.Log("[ERROR] Unknown Interaction Method Value!!");
                break;
        }
        GameObject.Find("MixedRealitySceneContent").GetComponent<Output_generator>().set_interact_method(int_method);
    }

    void disable_eye_tracking()
    {
        eye_tracking.SetActive(false);
    }

    void enable_eye_tracking()
    {
        eye_tracking.SetActive(true);
    }

    void disable_button_controls()
    {
        button_controls.SetActive(false);
    }

    void enable_button_controls()
    {
        button_controls.SetActive(true);
    }

    void disable_hand_controls()
    {
        hand_controls.SetActive(false); 
    }

    void enable_hand_control()
    {
        hand_controls.SetActive(true);
    }

    void disable_multi_screen()
    {
        multi_screen_go.GetComponent<screenManager>().unset_multi_screen();
    }

    void enable_multi_screen()
    {
        multi_screen_go.GetComponent<screenManager>().set_multi_screen();
    }
}
