using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    private class DialogString
    {
        [Header("Message")]
        public string text;
        public bool isEnd;

        [Header("Branch")]
        public bool isQuestion;
        public string answerOption1;
        public string answerOption2;
        public string answerOption3;
        public int option1IndexJump;
        public int option2IndexJump;
        public int option3IndexJump;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent endDialogueEvent;
    }

    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Button option3Button;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private List<dialogueString> dialogueList = new List<dialogueString>();

    private int currentDialogueIndex = 0;
    private bool initialized = false;
    private bool optionSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        // HideDialogWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialog()
    {
        if (initialized)
            return;

        initialized = true;

        DisableButtons();

        StartCoroutine(ProcessDialog());
    }

    public void HideDialogWindow()
    {
        dialogueParent.SetActive(false);
    }

    public void ShowDialogWindow()
    {
        dialogueParent.SetActive(true);
    }

    private IEnumerator ProcessDialog()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
            dialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvent?.Invoke();

            if(line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;
                option3Button.GetComponentInChildren<TMP_Text>().text = line.answerOption3;

                EnableButtons();

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                option3Button.onClick.AddListener(() => HandleOptionSelected(line.option3IndexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                line.endDialogueEvent?.Invoke();
                yield return StartCoroutine(TypeText(line.text));
            }

            
            
            optionSelected = false;
        }

        DialogueStop();
    }

    private void DisableButtons()
    {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        option3Button.gameObject.SetActive(false);
    }

    private void EnableButtons()
    {
        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);
        option3Button.gameObject.SetActive(true);
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();

        currentDialogueIndex = indexJump;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if(!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if(dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }

        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        HideDialogWindow();
    }
}
