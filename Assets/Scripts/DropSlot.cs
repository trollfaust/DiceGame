using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private SlotConfig myConfig;
    public TextMeshProUGUI valueText;
    public Sprite[] sprites;
    public Image image;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject colorSwap;

    [HideInInspector]
    public Draggable myDraggable;
    private QuestCard questCard;

    public void Setup(SlotConfig config)
    {
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        colorSwap.SetActive(false);

        myConfig = config;
        valueText.text = (myConfig.minValue > 0) ? myConfig.minValue.ToString() + "+" : "";
        image.sprite = sprites[(int)myConfig.color];
        if (myConfig.changeAmount > 0)
        {
            upArrow.SetActive(true);
            upArrow.GetComponent<Arrow>()?.Setup(myConfig);
        }
        if (myConfig.changeAmount < 0)
        {
            downArrow.SetActive(true);
            downArrow.GetComponent<Arrow>()?.Setup(myConfig);
        }

        if (myConfig.changeColor != FACECOLOR.none)
        {
            colorSwap.SetActive(true);
        }

        questCard = gameObject.GetComponentInParent<QuestCard>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (IsOccupied())
            return;

        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable == null)
            return;

        Dice dice = eventData.pointerDrag.GetComponent<Dice>();
        if (dice == null)
            return;

        if (dice.diceConfig.currentFace.value < myConfig.minValue || (dice.diceConfig.currentFace.color != myConfig.color && myConfig.color != FACECOLOR.none))
            return;

        draggable.myDropSlot = this;

        myDraggable = draggable;
        questCard?.SlotChanged();
    }

    public void DraggableLeft(Draggable draggable)
    {
        if (myDraggable == draggable)
        {
            myDraggable = null;
            questCard?.SlotChanged();
        }
    }

    public bool IsOccupied()
    {
        if (myDraggable != null)
            return true;
        return false;
    }

    public void OnComplete() 
    {
        Dice dice = myDraggable.GetComponent<Dice>();

        if (myConfig.changeAmount != 0)
        {
            dice.ChangeFaceValue(myConfig.changeAmount);
        }

        if (myConfig.changeColor != FACECOLOR.none)
        {
            dice.ChangeFaceColor(myConfig.changeColor);
        }

        myDraggable.SetToHomeSlot();
        dice.Roll();
    }
}
