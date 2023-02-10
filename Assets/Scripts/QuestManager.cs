using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager Instance;
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

    public GameObject cardPrefab;
    public Transform cardParent;
    public float delayBetweenCards = 0.3f;
    public int questsDone { get; private set; }

    private void Start()
    {
        questsDone = 0;
    }

    public void DrawCards(int amount)
    {
        StartCoroutine(DrawsCardDelayCoroutine(delayBetweenCards, amount));
    }

    public void DrawCard()
    {
        StartCoroutine(DrawsCardDelayCoroutine(delayBetweenCards, 1));
    }

    private void Draw()
    {
        GameObject newCard = Instantiate(cardPrefab, cardParent);
        QuestCard newQuestCard = newCard.GetComponent<QuestCard>();

        newQuestCard.Setup(QuestSetup.Instance.GetRandomQuest());
    }

    IEnumerator DrawsCardDelayCoroutine(float delay, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);
            Draw();
        }
    }

    public void QuestDone(Quest quest)
    {
        questsDone++;
    }
}
