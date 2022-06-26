using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputPanel : MonoBehaviour
{
    [SerializeField] Button exitButton;
    //public TextMeshProUGUI code;
    [SerializeField] TMP_InputField codeInput;
    [SerializeField] string answer;
    [SerializeField] TextMeshProUGUI debugLog;

    Interactable interactable;
    TouchScreenKeyboard keyboard;

    public void setInteractable(Interactable interactable)
    {
        this.interactable = interactable;
    }

    public void CheckInput(string code)
    {
        //debugLog.text = code + " Total Amount: " + code.Length + " Answer:" + answer + " : " + answer.Length;
        
        if (code == answer)
        {
            //debugLog.text = "CORRECT";
            code = "Correct";
            interactable.Interact();
            this.gameObject.SetActive(false);
        }
        else if (code.Length >= 5 && code != answer) 
        {
            //debugLog.text = keyboard.text + " Answer Wrong Total Amount: " + keyboard.text.Length;
            codeInput.text = "";
        }  
    }

    // Update is called once per frame
    void Update()
    {
           // CheckInput();
    }
}
