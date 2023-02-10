using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public static int faceCount = 6;
    public float rollTime = 0.2f;
    public int rollAmount = 30;
    public Image diceImage;
    public DiceConfig diceConfig;

    private Draggable myDraggable;

    private void Start()
    {
        myDraggable = gameObject.GetComponent<Draggable>();

        Roll();
    }

    public void Setup(DiceConfig diceConfig)
    {
        this.diceConfig = diceConfig;

        SetFace(diceConfig.faces[0]);
    }

    public void ChangeFaceValue(int changeAmount)
    {
        DieFace newFace = DiceManager.Instance.ChangeFaceByAmount(diceConfig.currentFace, changeAmount);
        diceConfig.faces[System.Array.IndexOf<DieFace>(diceConfig.faces, diceConfig.currentFace)] = newFace;
        SetFace(newFace);
    }

    public void ChangeFaceColor(FACECOLOR newColor)
    {
        if (newColor == FACECOLOR.swap)
        {
            newColor = diceConfig.currentFace.color + 1;
            newColor = (newColor == FACECOLOR.swap) ? FACECOLOR.blue : newColor;
        }

        DieFace newFace = DiceManager.Instance.ChangeFaceColor(diceConfig.currentFace, newColor);
        diceConfig.faces[System.Array.IndexOf<DieFace>(diceConfig.faces, diceConfig.currentFace)] = newFace;
        SetFace(newFace);
    }

    public void Roll()
    {
        if (myDraggable.isDragged || !myDraggable.isInHomeSlot)
            return;

        myDraggable.isDraggable = false;
        StartCoroutine(RollRoutine());
    }

    IEnumerator RollRoutine()
    {
        int lastIndex = 0;
        for (int i = 0; i < rollAmount; i++)
        {
            int rng = Random.Range(0, diceConfig.faces.Length);
            if (lastIndex == rng)
            {
                rng++;
                if (rng >= diceConfig.faces.Length)
                    rng -= 2;
            }
            diceImage.gameObject.transform.rotation = Quaternion.Euler(0, 0, (360 / rollAmount) * (i + 1) * 1);
            SetVisuals(diceConfig.faces[rng]);
            lastIndex = rng;
            yield return new WaitForSeconds(rollTime);
        }

        int roll = Random.Range(0, diceConfig.faces.Length);
        SetFace(diceConfig.faces[roll]);

        myDraggable.isDraggable = true;
    }

    void SetFace(DieFace face)
    {
        diceConfig.currentFace = face;
        SetVisuals(face);
    }

    private void SetVisuals(DieFace face)
    {
        diceImage.sprite = face.sprite;
    }

}
