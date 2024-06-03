using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Author>
/// Lane O'Rafferty
/// </Author>
/// <summary>
/// This class is responsible for populating the book's tasks.
/// </summary>
/// <remarks>
/// Unused.
/// </remarks>
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
