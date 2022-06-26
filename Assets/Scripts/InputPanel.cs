using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputPanel : MonoBehaviour
{
    [SerializeField] Button exitButton;
    public TextMeshProUGUI code;

    [SerializeField] string answer;
    [SerializeField] TextMeshProUGUI debugLog;

    Interactable interactable;
    TouchScreenKeyboard keyboard;

    private void Start() {
        //TouchScreenKeyboard.hideInput = true;
        keyboard.characterLimit = 5;
    }
    public void setInteractable(Interactable interactable)
    {
        this.interactable = interactable;
    }

    void CheckInput()
    {
        code.text = keyboard.text;
        if (code.text == answer)
        {
            //debugLog.text = "CORRECT";
            code.text = "Correct";
            interactable.Interact();
            this.gameObject.SetActive(false);
        }
        else if (code.text.Length >= 4 && code.text != answer) 
        {
            debugLog.text = code.text + " Total Amount: " + code.text.Length;
            code.text = " ";
            TouchScreenKeyboard.Open(code.text);
        }  
    }

    // Update is called once per frame
    void Update()
    {
            CheckInput();
    }
}
