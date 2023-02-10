using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Vector3 offset;
    private Image myImage;
    private Canvas canvas;
    public DropSlot myDropSlot;
    public DropSlot homeDropSlot;
    public bool isDraggable = true;
    public bool isDragged { get; private set; } = false;
    public bool isInHomeSlot { get; private set; } = true;

    public void Setup(DropSlot slot)
    {
        myImage = gameObject.GetComponentInChildren<Image>();
        canvas = gameObject.GetComponentInParent<Canvas>();

        homeDropSlot = slot;
        myDropSlot = slot;
        SnapToSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;
        isDragged = true;

        offset = transform.position - (Vector3)eventData.position;
        myImage.raycastTarget = false;
        transform.SetParent(canvas.transform);
        isInHomeSlot = false;
        myDropSlot.DraggableLeft(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        transform.position = (Vector3)eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;
        isDragged = false;

        if (eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<DropSlot>() == null)
        {
            SetToHomeSlot();
        }

        if (DiceManager.Instance.CheckIsStartSlot(myDropSlot) && myDropSlot != homeDropSlot)
        {
            SetToHomeSlot();
        }

        SnapToSlot();
        
    }

    public void SetToHomeSlot()
    {
        myDropSlot = homeDropSlot;
        SnapToSlot();
    }

    void SnapToSlot()
    {
        myImage.raycastTarget = true;
        offset = Vector3.zero;

        transform.SetParent(myDropSlot.transform);
        transform.localPosition = Vector3.zero;

        if (myDropSlot == homeDropSlot)
            isInHomeSlot = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDragged)
            return;

        Tooltip.Instance.Setup(gameObject.GetComponent<Dice>().diceConfig);
    }
}
