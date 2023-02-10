using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestCard : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public TextMeshProUGUI title;
    public TextMeshProUGUI effectText;
    private Quest quest;
    private List<DropSlot> mySlots = new List<DropSlot>();

    public void Setup(Quest quest)
    {
        this.quest = quest;
        title.text = quest.title;
        effectText.text = quest.effectText;

        foreach (SlotConfig slot in quest.slots)
        {
            GameObject newSlotGO = Instantiate(slotPrefab, slotParent);
            DropSlot newDropSlot = newSlotGO.GetComponent<DropSlot>();
            newDropSlot.Setup(slot);
            mySlots.Add(newDropSlot);
        }
    }

    public void SlotChanged()
    {
        foreach (DropSlot slot in mySlots)
        {
            if (!slot.IsOccupied())
                return;
        }

        StartCoroutine(CompleteQuest());
    }

    IEnumerator CompleteQuest()
    {
        yield return new WaitForSeconds(0.1f);

        quest.Complete();
        QuestManager.Instance.QuestDone(quest);

        yield return new WaitForSeconds(0.2f);

        foreach (DropSlot slot in mySlots)
        {
            slot.OnComplete();
        }

        yield return new WaitForSeconds(0.2f);

        QuestManager.Instance.DrawCard();
        Destroy(gameObject);
       
    }

}
