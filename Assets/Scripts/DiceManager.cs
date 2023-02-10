using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    #region Singleton
    public static DiceManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public GameObject dicePrefab;
    public DropSlot[] startDieSlots;
    public int startRolls = 11;

    private DiceConfig[] startDiceConfigs;

    private List<Dice> dices = new List<Dice>();

    public int rollsLeft { get; private set; }

    public void Setup()
    {
        startDiceConfigs = DiceSetup.Instance.GetStartingDice();
        rollsLeft = startRolls;

        if (startDieSlots.Length < startDiceConfigs.Length)
        {
            Debug.LogError("To many Start Dice!!");
            return;
        }

        foreach (DropSlot slot in startDieSlots)
        {
            slot.Setup(new SlotConfig() );
        }

        for (int i = 0; i < startDiceConfigs.Length; i++)
        {
            SpawnDice(startDiceConfigs[i]);
        }
    }

    public bool CheckIsStartSlot(DropSlot slotToCheck)
    {
        foreach (DropSlot slot in startDieSlots)
        {
            if (slot == slotToCheck)
            {
                return true;
            }
        }
        return false;
    }

    public void RollAllDice()
    {
        if (rollsLeft <= 0)
            return;

        rollsLeft--;
        foreach (Dice dice in dices)
        {
            dice.Roll();
        }
    }

    public DieFace ChangeFaceByAmount(DieFace face, int amount)
    {
        int min = GetFirstDieFaceIndexByColor(face.color);
        int max = GetLastDieFaceIndexByColor(face.color);
        int index = System.Array.IndexOf<DieFace>(DiceSetup.Instance.allFaces, face) + amount;

        if (index < min)
            index = min;

        if (index >= max)
            index = max;

        return DiceSetup.Instance.allFaces[index];
    }

    public DieFace ChangeFaceColor(DieFace face, FACECOLOR newColor)
    {
        int minOld = GetFirstDieFaceIndexByColor(face.color);
        int minNew = GetFirstDieFaceIndexByColor(newColor);
        int maxNew = GetLastDieFaceIndexByColor(newColor);

        int index = (System.Array.IndexOf<DieFace>(DiceSetup.Instance.allFaces, face) - minOld) + minNew;

        if (index < minNew)
            index = minNew;

        if (index >= maxNew)
            index = maxNew;

        return DiceSetup.Instance.allFaces[index];
    }

    private int GetFirstDieFaceIndexByColor(FACECOLOR color)
    {
        for (int i = 0; i < DiceSetup.Instance.allFaces.Length; i++)
        {
            if (DiceSetup.Instance.allFaces[i].color == color)
            {
                return i;
            }
        }
        return 0;
    }

    private int GetLastDieFaceIndexByColor(FACECOLOR color)
    {
        bool isCorrectColor = false;
        for (int i = 0; i < DiceSetup.Instance.allFaces.Length; i++)
        {
            if (DiceSetup.Instance.allFaces[i].color == color)
            {
                isCorrectColor = true;
            }
            if (DiceSetup.Instance.allFaces[i].color != color && isCorrectColor)
            {
                return i - 1;
            }
        }
        return DiceSetup.Instance.allFaces.Length - 1;
    }

    public void SpawnDice(DiceConfig diceConfig)
    {
        DropSlot dropSlot = GetNextDropSlot();
        if (dropSlot == null)
            return;

        GameObject newDiceGO = Instantiate(dicePrefab, dropSlot.transform);
        Dice newDice = newDiceGO.GetComponent<Dice>();

        dices.Add(newDice);
        newDice.Setup(diceConfig);

        newDiceGO.GetComponent<Draggable>().Setup(dropSlot);
    }

    private DropSlot GetNextDropSlot()
    {
        if (dices.Count >= startDieSlots.Length)
            return null;

        DropSlot dropSlot = startDieSlots[dices.Count];
        return dropSlot;
    }
}
