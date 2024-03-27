using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domophone : MonoBehaviour {
    //text mesh pro buttons
    [SerializeField] Button[] buttons;
    [SerializeField] Sprite[] digitSprites; // 0-9
    [SerializeField] Image[] digits; // 4 digits

    int[] code = new int[4];

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
        for (int i = 0; i < code.Length - 1; i++) {
            code[i] = code[i + 1];
        }
        code[code.Length - 1] = digit;

        for(int i=0; i<code.Length; i++)
            digits[i].sprite = digitSprites[code[i]];
    }
}
