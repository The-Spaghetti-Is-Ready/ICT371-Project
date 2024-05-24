using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionaireManagement : MonoBehaviour
{
    public List<QuestionAnswer> QnA;
    public GameObject[] option;
    public int currentQuestion;

    public Text Questiontext;

    private void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
        for(int i = 0; i < option.Length; i++)
        {

            option[i].GetComponent<AnswerScript>().isCorrect = false;
            option[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answer[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                option[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        try
        {
            currentQuestion = UnityEngine.Random.Range(0, QnA.Count);

            Questiontext.text = QnA[currentQuestion].Question;
            SetAnswer();
        }
        catch(Exception e)
        {
            print("Questionnaire finished");
        }
    }
}
