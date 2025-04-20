using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.MixedReality.Toolkit.Experimental.InteractiveElement;
using static UnityEngine.ParticleSystem;

public class Output_generator : MonoBehaviour
{
    int correct_answers;
    int incorrect_answers;

    int click_counter;

    int swap_counter;

    float trial_time = 0.0f;

    string subj_num = "0";

    string interaction_method;

    string[] lines = new string[1];

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "s" + subj_num + "_data.csv");
        string content = "subj, interaction method, clicks, incorrect_count, accuracy, swaps, trial_length";
        lines[0] = content;
        File.WriteAllLines(path, lines);
    }

    private void Update()
    {
        trial_time += Time.deltaTime;
    }

    public void check_answer(bool ans, string person, string hour, string date)
    {
        int h = int.Parse(hour);
        int d = 0;
        if(date.Length > 3) d = int.Parse(date.Substring(0,2));
        else d = int.Parse(date.Substring(0,1));

        bool real_answer = false;
        if (person.Contains("and")) real_answer = get_both_availability(d, h);
        else if (person.Contains("Davey")) real_answer = get_availability(1, d, h);
        else real_answer = get_availability(2, d, h);

        Debug.Log("CHECKING");

        if (real_answer == ans)
        {
            Debug.Log("CORRECT!!!");
            correct_answers++;
        }
        else
        {
            Debug.Log("INcorrect :'(");
            incorrect_answers++;
        }
    }

    string[] append_array(string new_val)
    {
        string[] newArray = new string[lines.Length + 1];
        for (int i = 0; i < lines.Length; i++)
        {
            newArray[i] = lines[i];
        }
        newArray[newArray.Length - 1] = new_val;
        return newArray;
    }

    /*
     * ToDos:
     * Get all buttons hooked up to click_count()
     * Count number of swaps here
     * Calculate trial time
     * MAKE SURE that trial time starts when the first trial actually starts
     * Get handedness
     * Get subject number
     * Set everything up for the trial run
     * Test output file
     * Add to output file after each trial
     * Control method
     * Testing rounds
     */
    public void make_output_file(bool training)
    {
        if (!training)
        {
            string path = Path.Combine(Application.persistentDataPath, "s" + subj_num + "_data.csv");
            string content = subj_num + ", " + interaction_method + ", " + click_counter.ToString() + ", " + incorrect_answers + ", " + accuracy_calc().ToString() + ", " + GetComponent<screenManager>().get_swaps().ToString() + ", " + trial_time.ToString();
            lines = append_array(content);
            File.WriteAllLines(path, lines);
            Debug.Log("File Exists: " + File.Exists(path).ToString());

            Debug.Log(content);
        }

        click_counter = 0;
        incorrect_answers = 0;
        correct_answers = 0;
        swap_counter = 0;
        trial_time = 0f;
    }

    float accuracy_calc()
    {
        float acc = (float)correct_answers / (correct_answers + incorrect_answers);
        Debug.Log("ACC: " + acc.ToString());
        Debug.Log("Correct: " + correct_answers.ToString());
        Debug.Log("incorrect: " + incorrect_answers.ToString());
        return acc;
    }

    public bool get_availability(int index, int day, int hour)
    {
        return transform.GetChild(index).GetChild(day).GetChild(6).GetComponent<randomize_slots>().get_av(hour);
    }

    public bool get_both_availability(int day, int hour)
    {
        bool a1 = get_availability(1, day, hour);
        bool a2 = get_availability(2, day, hour);
        return a1 && a2;
    }

    public void click_count()
    {
        click_counter++;
    }

    public void reset_trial_time()
    {
        trial_time = 0f;
    }

    public void set_interact_method(string method)
    {
        interaction_method = method;
    }

    public void Set_subj(string subj)
    {
        subj_num = subj;
    }
}
