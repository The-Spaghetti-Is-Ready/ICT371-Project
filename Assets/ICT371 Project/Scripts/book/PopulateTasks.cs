using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateTasks : MonoBehaviour
{
    [SerializeField]
    BookInterface _book;

    public void Populate(List<string> activityNames)
    {
        for (int i = 0; i < activityNames.Count && i < 3; i++)
        {
            _book.SetTaskText(i + 1, activityNames[i]);
        }
    }
}
