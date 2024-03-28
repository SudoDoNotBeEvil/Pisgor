using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Domophone : MonoBehaviour {
    //text mesh pro buttons
    [SerializeField] Button[] buttons;
    [SerializeField] Sprite[] digitSprites; // 0-9
    [SerializeField] Image[] digits; // 4 digits

    int[] code = new int[4];

    int digitsEntered = 0;
    bool isReadyToEnter = true;

    [SerializeField] UnityEvent onRightAnswer;

    void Awake() {
        gameObject.SetActive(false);
    }

    public void Use() {
        gameObject.SetActive(true);
    }

    void Start() {
        for (int i = 0; i < 10; i++) {
            int buttonIndex = i; // store the current value of 'i' in a separate variable
            buttons[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
    }

    void ButtonClicked(int buttonIndex) {
        NewDigitEntered(buttonIndex);

        Debug.Log($"Button {buttonIndex} Clicked");
        //digits[0].sprite = digitSprites[buttonIndex];
    }
    
    new void NewDigitEntered(int digit) {
        if (!isReadyToEnter)
            return;

        digitsEntered++;
        Debug.Log($"digitsEntered: {digitsEntered}");

        for (int i = code.Length - 1; i > 0; i--) {
            code[i] = code[i - 1];
        }
        code[0] = digit;
        
        RedrawScreen();

        if (digitsEntered == 4) {
            isReadyToEnter = false;
            StartCoroutine(CheckCode());
        }
    }

    private void RedrawScreen() {
        for (int i = 0; i < code.Length; i++)
            digits[i].sprite = digitSprites[code[i]];
    }

    IEnumerator CheckCode() {
        yield return new WaitForSeconds(0.5f);

        var rightCode = PersistentCodeGenerator.Instance.GetCode();
        bool isRight = true;
        for (int i = 0; i < 4; i++)
        {
            if (code[i] != rightCode[i]) {
                isRight = false;
                break;
            }
        }

        ResetDisplayAndEnteredCode();

        if (isRight) {
            Debug.Log("The code is right");
            onRightAnswer?.Invoke();
        }
        else {
            Debug.Log("Wrong code");
        }

        yield return new WaitForSeconds(0.1f);
        isReadyToEnter = true;
        gameObject.SetActive(false);
    }

    private void ResetDisplayAndEnteredCode() {
        for (int i = 0; i < digits.Length; i++) {
            digits[i].sprite = digitSprites[0];
        }

        for (int i = 0; i < code.Length; i++) {
            code[i] = 0;
        }

        digitsEntered = 0;
    }
}
