using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Roll (" + (DiceManager.Instance.startRolls).ToString() + " left)";
    }

    public void OnClick()
    {
        DiceManager.Instance.RollAllDice();
        buttonText.text = "Roll (" + DiceManager.Instance.rollsLeft.ToString() + " left)";
    }
}
