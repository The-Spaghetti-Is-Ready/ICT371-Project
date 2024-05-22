using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSystem : MonoBehaviour
{
    private List<Day> days;


    // Start is called before the first frame update
    void setDays(List<Day> days2)
    {
        days = days2;
    }

    void setDayAt(int indx, Day dayInsert)
    {
        if(indx != days.Count)
        {
            days.Insert(indx, dayInsert);
        }
    }

    // Update is called once per frame
    int getCurrentDay()
    {
        return 0;
    }
}
