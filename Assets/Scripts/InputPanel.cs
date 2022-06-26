using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputPanel : MonoBehaviour
{
    [SerializeField] Button exitButton;
    public TextMeshProUGUI numbers;

    [SerializeField] string answer;
    [SerializeField] TextMeshProUGUI debugLog;

    Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        PushTheButton.ButtonPressed += AddDigit;
    }

    private void AddDigit(string digit)
    {
        numbers.text += digit;
    }

    private void Dismiss()
    {
        //GateLock.SetActive(false);
    } 

    public void setInteractable(Interactable interactable)
    {
        this.interactable = interactable;
    }

    void CheckInput()
    {
        if (numbers.text == "1234")
        {
            debugLog.text = "CORRECT";
            numbers.text = "Correct";
            interactable.Interact();
            this.gameObject.SetActive(false);
        }
        else if (numbers.text.Length >= 4 && numbers.text != "1234") 
        {
            numbers.text = "";
        }  
    }

    // Update is called once per frame
    void Update()
    {
            CheckInput();
    }
}
