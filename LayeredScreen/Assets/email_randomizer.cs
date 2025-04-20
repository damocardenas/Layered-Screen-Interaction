using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class email_randomizer : MonoBehaviour
{
    [SerializeField]
    string[] menu_people;
    string[] current_menu_person;

    [SerializeField]
    string[] email_people;
    string[] current_email_people;

    string[] current_hour;

    string[] current_date;

    bool[] answers;

    int answer_counter = 0;

    [SerializeField]
    int[] menu_seed = new int[8];
    int seed_index = 0;

    bool first_out = true;

    int total_trial_count = 0;

    int max_trials = 8;

    bool training = true;

    // Start is called before the first frame update
    void Start()
    {
        current_email_people = new string[transform.childCount - 1];
        current_menu_person = new string[transform.childCount - 1];
        current_hour = new string[transform.childCount - 1];
        current_date = new string[transform.childCount - 1];
        answers = new bool[transform.childCount-1];
        randomize_emails();
        for (int i = 1; i < transform.childCount; i++) transform.GetChild(i).GetChild(6).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = randomize_string(i-1);
    }

    public void Set_trial(bool real)
    {
        if (real)
        {
            training = false;
            max_trials = 8;
        }
        else
        {
            max_trials = 4;
        }
    }

    public void generate_from_beginning()
    {
        answer_counter = 0;
        randomize_emails();
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<buttonOpacity>().reactivate();
            transform.GetChild(i).GetChild(6).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = randomize_string(i - 1);
        }
    }

    public string randomize_string(int ind)
    {

        if (menu_seed[seed_index] == 2)
        {
            current_menu_person[ind] = "Are " + menu_people[0] + " and " + menu_people[1];
        }
        else current_menu_person[ind] = "Is " + menu_people[menu_seed[seed_index]];
        seed_index++;

        current_hour[ind] = UnityEngine.Random.Range(1, 9).ToString();

        current_date[ind] = format_num(UnityEngine.Random.Range(1, 29));

        return current_menu_person[ind] + " available at " + current_hour[ind] + " pm on the " + current_date[ind] + "?";
    }

    string format_num(int num)
    {
        if (num == 1 || num == 21) return num.ToString() + "st";
        if (num == 2 || num == 22) return num.ToString() + "nd";
        if (num == 3 || num == 23) return num.ToString() + "rd";
        return num.ToString() + "th";
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

    public void randomize_emails()
    {
        Debug.Log(seed_index);

        if (seed_index >= menu_seed.Length) seed_index = 0;

        int[] temp = menu_seed.Skip(seed_index).Take(4).ToArray();
        System.Random shuf = new System.Random();
        temp = Shuffle(shuf, temp);
        for (int i = 0; i < temp.Length; i++)
        {
            menu_seed[i + seed_index] = temp[i];
        }

        for (int i = 1; i < transform.childCount; i++)
        {
            bool already_used = true;
            while (already_used)
            {
                string temp_email_guy = email_people[UnityEngine.Random.Range(0, email_people.Length)];
                already_used = false;
                for (int j = 0; j < current_email_people.Length; j++)
                {
                    if (current_email_people[j] == temp_email_guy)
                    {
                        already_used = true;
                    }
                }
                if (!already_used)
                {
                    current_email_people[i - 1] = temp_email_guy;
                    transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = temp_email_guy;
                }
            }
        }
    }

    public void answer_yes(int child_index)
    {
        total_trial_count++;
        child_index++;
        answer_counter++;
        answers[child_index-1] = true;
        transform.parent.gameObject.GetComponent<Output_generator>().check_answer(true, current_menu_person[child_index - 1], current_hour[child_index - 1], current_date[child_index - 1]);
        transform.GetChild(child_index).GetComponent<menu_guy>().turn_menu_off();
        transform.GetChild(child_index).GetComponent<buttonOpacity>().deactivate();
        GetComponent<screenOpacity>().is_off();
        if (answer_counter > 3)
        {
            if (total_trial_count == max_trials)
            {
                seed_index = 0;
            }
            generate_from_beginning();
            Debug.Log("DONE!!");
        }
        if (total_trial_count == max_trials)
        {
            transform.parent.GetComponent<Output_generator>().make_output_file(training);
            first_out = false;
            GameObject.Find("Trial ordering").GetComponent<trial_guy>().next_trial();
            total_trial_count = 0;
        }

    }

    public void answer_no(int child_index)
    {
        total_trial_count++;
        child_index++;
        answer_counter++;
        answers[child_index-1] = false;
        transform.parent.gameObject.GetComponent<Output_generator>().check_answer(false, current_menu_person[child_index - 1], current_hour[child_index - 1], current_date[child_index - 1]);
        transform.GetChild(child_index).GetComponent<menu_guy>().turn_menu_off();
        transform.GetChild(child_index).GetComponent<buttonOpacity>().deactivate();
        GetComponent<screenOpacity>().is_off();
        if (answer_counter > 3)
        {
            if (total_trial_count == max_trials)
            {
                seed_index = 0;
            }
            generate_from_beginning();
            Debug.Log("DONE!!");
        }
        if (total_trial_count == max_trials)
        {
            transform.parent.GetComponent<Output_generator>().make_output_file(training);
            first_out = false;
            GameObject.Find("Trial ordering").GetComponent<trial_guy>().next_trial();
            total_trial_count = 0;
        }

    }
}
