using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QUESTDIFFICULTY { supereasy, easy, normal, hard, hardcore }

public class QuestSetup : MonoBehaviour
{
    #region Singleton
    public static QuestSetup Instance;
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

    public List<Quest> quests = new List<Quest>();

    private void Start()
    {
        SetupQuests();
    }

    public void SetupQuests()
    {
        quests.Add(new Quest("Quest 1", "", QUESTDIFFICULTY.easy, new SlotConfig[] { new SlotConfig(0, FACECOLOR.none, 1, FACECOLOR.blue), new SlotConfig(2, FACECOLOR.blue) }, () => {  } ));
        quests.Add(new Quest("Quest 2", "", QUESTDIFFICULTY.easy, new SlotConfig[] { new SlotConfig(0, FACECOLOR.none, 2, FACECOLOR.red), new SlotConfig(2, FACECOLOR.blue) }, () => {  }));
        quests.Add(new Quest("Quest 3", "", QUESTDIFFICULTY.easy, new SlotConfig[] { new SlotConfig(0, FACECOLOR.none, 3, FACECOLOR.red), new SlotConfig(2, FACECOLOR.blue) }, () => {  }));
        quests.Add(new Quest("Dice Quest", "Spawns new Dice", QUESTDIFFICULTY.easy, new SlotConfig[] { new SlotConfig(0, FACECOLOR.red), new SlotConfig(2, FACECOLOR.blue) }, () => { 
            DiceManager.Instance.SpawnDice(new DiceConfig(new DieFace[] {
                DiceSetup.Instance.allFaces[1], DiceSetup.Instance.allFaces[2], DiceSetup.Instance.allFaces[2], DiceSetup.Instance.allFaces[2], DiceSetup.Instance.allFaces[2], DiceSetup.Instance.allFaces[3] 
            }));
        }));



        QuestManager.Instance.DrawCards(3);
    }

    public Quest GetRandomQuest()
    {
        return quests[Random.Range(0, quests.Count)];
    }

    public Quest GetRandomQuestInDifficultyRange(QUESTDIFFICULTY difficultyLow, QUESTDIFFICULTY difficultyHigh)
    {
        List<Quest> difficultyQuests = new List<Quest>();

        foreach (Quest quest in quests)
        {
            if ((int)quest.difficulty >= (int)difficultyLow && (int)quest.difficulty <= (int)difficultyHigh)
            {
                difficultyQuests.Add(quest);
            }
        }
        return difficultyQuests[Random.Range(0, difficultyQuests.Count)];
    }

    public Quest GetRandomQuestWithDifficulty(QUESTDIFFICULTY difficulty)
    {
        return GetRandomQuestInDifficultyRange(difficulty, difficulty);
    }

    public Quest GetRandomQuestUpToDifficulty(QUESTDIFFICULTY difficulty)
    {
        return GetRandomQuestInDifficultyRange(0, difficulty);
    }
}
