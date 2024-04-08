using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameToggle : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject cardsCamera;
    [SerializeField] private GameObject cardGameUI;

    // Start is called before the first frame update
    void Start()
    {
        cardGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCardGame()
    {
        playerCamera.SetActive(false);
        cardsCamera.SetActive(true);
        cardGameUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndCardGame()
    {
        playerCamera.SetActive(true);
        cardsCamera.SetActive(false);
        cardGameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
