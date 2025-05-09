using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trial_guy : MonoBehaviour
{

    string subj_num = "";
    int trial_step = 0;

    [SerializeField]
    GameObject scene_content;
    [SerializeField]
    GameObject interaction_methods;

    int[] interact_order;
    int current_method_index = 0;

    public bool is_right_handed;
    int trial_counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = "[Enter Subject Number]";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject headset = GameObject.Find("Main Camera");
            transform.position = transform.position + headset.transform.position;
        }

        switch (trial_step)
        {
            case 0:
                subj_update();
                break;
            case 1:
                GameObject.Find("Screen1").GetComponent<email_randomizer>().Set_trial(false);
                interact_order = randomize_order();
                
                Debug.Log("DAMO DEBUG: " + "0= " + interact_order[0] + "\n1= " + interact_order[1] + "\n2= " + interact_order[2] + "\n3= " + interact_order[3]);
                //Interaction order is list of ints and the ints represent interaction methods

                trial_step++;
                interaction_methods.GetComponent<interactionMethodController>().change_method(interact_order[current_method_index]);
                current_method_index++;
                trial_counter++;
                //TUTOR MENUS NEED SOMETHING TO START IT HERE BECAUSE IT DOESN'T CALL CONT_TRIAL AT THE BEGINNING
                // The way we go from here to next trial is from another script call, don't need know it
                getFirstTutor();
                break;
            case 3:
                GameObject.Find("Screen1").GetComponent<email_randomizer>().Set_trial(true);
                interact_order = randomize_order();
                trial_step++;
                interaction_methods.GetComponent<interactionMethodController>().change_method(interact_order[current_method_index]);
                current_method_index++;
                break;

        }
    }

    void subj_update()
    {
        get_curr_num();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            finish_subj_num();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            subj_num = string.Empty;
            transform.GetChild(0).GetComponent<TMP_Text>().text = "[Enter Subject Number]";
        }
    }

    void get_curr_num()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) subj_num += "0";
        else if (Input.GetKeyDown(KeyCode.Alpha1)) subj_num += "1";
        else if (Input.GetKeyDown(KeyCode.Alpha2)) subj_num += "2";
        else if (Input.GetKeyDown(KeyCode.Alpha3)) subj_num += "3";
        else if (Input.GetKeyDown(KeyCode.Alpha4)) subj_num += "4";
        else if (Input.GetKeyDown(KeyCode.Alpha5)) subj_num += "5";
        else if (Input.GetKeyDown(KeyCode.Alpha6)) subj_num += "6";
        else if (Input.GetKeyDown(KeyCode.Alpha7)) subj_num += "7";
        else if (Input.GetKeyDown(KeyCode.Alpha8)) subj_num += "8";
        else if (Input.GetKeyDown(KeyCode.Alpha9)) subj_num += "9";

        if (subj_num.Length != 0)
        {
            transform.GetChild(0).GetComponent<TMP_Text>().text = subj_num;
        }
    }

    void finish_subj_num()
    {
        transform.GetChild(0).gameObject.SetActive(false);

        transform.GetChild(3).gameObject.SetActive(true);
        scene_content.GetComponent<Output_generator>().Set_subj(subj_num);
    }
    public int[] Shuffle(System.Random rng, int[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            int temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        return array;
    }

    public void Set_hand(bool right)
    {
        is_right_handed = right;
        scene_content.SetActive(true);
        interaction_methods.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
        trial_step++;
        GameObject.Find("Hand tracking items").GetComponent<handTracking>().set_hand(right);
    }

    int[] randomize_order()
    {

        scene_content.transform.GetChild(1).GetComponent<screenOpacity>().regen_all();
        scene_content.transform.GetChild(2).GetComponent<screenOpacity>().regen_all();
        current_method_index = 0;
        int[] temp = { 0, 1, 2, 3 };
        System.Random shuf = new System.Random();
        temp = Shuffle(shuf, temp);
        return temp;
    }

    public void next_trial() //Is the end of a trial
    {
        interaction_methods.GetComponent<interactionMethodController>().disable_last_method();
        scene_content.SetActive(false);
        if (trial_counter == interact_order.Length) transform.GetChild(4).gameObject.SetActive(true);
        else if (trial_counter > interact_order.Length) transform.GetChild(1).gameObject.SetActive(true);
        //THIS IS WHERE TUTORIAL MENUS SHOULD GO!!!!!!!!
        //BUTTON PRESS SHOULD BE AN EVENT THAT TRIGGERS CONT TRAIL
        //NEED TO FIGURE THE WAY THIS SCRIPT KEEPS TRACK OF RANDOM INTERACTION AND DISPLAY THAT MENU
        else if (trial_counter < interact_order.Length) {
            Debug.Log("DAMO DEBUG: HI! AM I WHERE i AM SUPPOSE TO BE??");
            getPostTutor();
        }
        else cont_trial();
    }

/*
 * DAMYENN NOTES FOR TUTOR MENUS
 * Left some comments on where something things need to go
 * So steps to complete the trial stuff
 * XXXX 1) Create and write the interaction descriptions
 * XXXX 2) Figure out the order of the interaction methods
 * XXXX 3) Use that order to display the first tutor menu that will explain the first trial of the practice
 * 4) Then use the continue button in the scene as an event to call cont_trial
 * 5) That will start the practice round for that interaction
 * 6) Once that ends, we should go back to the function above (or in the switch statement)
 * 7) Inside whatever we are, we need to then display the next tutor menu
 * 8) It should repeat until training is over
 * 9) Tutor menus should not appear during actual experiment
 * 
 * 
 * Ask matthew about the interaction order because I got nothing
 * Have good start code with where certain aspects need to go (When you call your method and When you finish a practice)
 * 
 * Bad part is the tutor screens call cont_trial
 * Need to figure out if the screens should appear at the end of a trial or at the beginning of the trial
 *  This impacts if we turn them off or if we call cont_trial, etc.
 *  
 * Super duper close!
 * 
 * 
 * Have a good chunk of it working!!! The main problem is that correct tutor menu is popping up after you finish the method. So might need to increase the number for post function?
 * 
 */

    public void cont_trial() //Is the beginning of a trial
    {
        scene_content.GetComponent<screenManager>().get_swaps();
        if (trial_counter > (interact_order.Length*2) - 1)
        {

            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            return;
        }
        if (trial_counter == interact_order.Length)
        {
            current_method_index = 0;
            //interact_order = randomize_order();
            trial_step++;
        }
        scene_content.SetActive(true);
        transform.GetChild (1).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive (false);
        interaction_methods.GetComponent<interactionMethodController>().enable_next_method(interact_order[current_method_index]);
        scene_content.GetComponent<Output_generator>().reset_trial_time();
        current_method_index++;
        trial_counter++;
        scene_content.transform.GetChild(1).GetComponent<screenOpacity>().regen_all(); 
        scene_content.transform.GetChild(2).GetComponent<screenOpacity>().regen_all();
    }

    public void getFirstTutor() {
        Debug.Log("DAMO DEBUG: FIRST TUTOR MENU\ninteract_order[current_method_index - 1]=" + interact_order[current_method_index - 1]);
        switch (interact_order[current_method_index - 1]) {
            case 0: //Eye Tracking
                GameObject.Find("FirstTrialTutorials").transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Eye Tracking");
                break;
            case 1: //Buttons
                GameObject.Find("FirstTrialTutorials").transform.GetChild(1).gameObject.SetActive(true);
                Debug.Log("Buttons");
                break;
            case 2: //Scroll
                GameObject.Find("FirstTrialTutorials").transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("Scroll");
                break;
            case 3: //Multi-window
                GameObject.Find("FirstTrialTutorials").transform.GetChild(3).gameObject.SetActive(true);
                Debug.Log("Window");
                break;
        }
    }

    public void getPostTutor()
    {
        Debug.Log("DAMO DEBUG: FIRST TUTOR MENU\ninteract_order[current_method_index]=" + interact_order[current_method_index]);
        switch (interact_order[current_method_index])
        {
            case 0: //Eye Tracking
                GameObject.Find("TrialTutorials").transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("Eye Tracking");
                break;
            case 1: //Buttons
                GameObject.Find("TrialTutorials").transform.GetChild(1).gameObject.SetActive(true);
                Debug.Log("Buttons");
                break;
            case 2: //Scroll
                GameObject.Find("TrialTutorials").transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("Scroll");
                break;
            case 3: //Multi-window
                GameObject.Find("TrialTutorials").transform.GetChild(3).gameObject.SetActive(true);
                Debug.Log("Window");
                break;
        }
    }
}
