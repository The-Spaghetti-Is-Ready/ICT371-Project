using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuestionaireManagement quizmanager;

    public void Answer()
    {

        if (isCorrect)
        {
            print("Correct");

            quizmanager.correct();
        }
        else
        {
            print("Wrong");

            quizmanager.correct();
        }
    }
}
