using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSetup : MonoBehaviour
{
    #region Singleton
    public static DiceSetup Instance;
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

    private List<DiceConfig> startDice = new List<DiceConfig>();
    public DieFace[] allFaces;
    public DiceConfig[] GetStartingDice()
    {
        return startDice.ToArray();
    }

    private void Start()
    {
        Setup();
        DiceManager.Instance.Setup();
    }

    void Setup()
    {
        startDice.Add(new DiceConfig( new DieFace[] { allFaces[1], allFaces[2], allFaces[2], allFaces[2], allFaces[2], allFaces[3] } ));
        startDice.Add(new DiceConfig(new DieFace[] { allFaces[1], allFaces[1], allFaces[1], allFaces[2], allFaces[2], allFaces[3] }));
    }
}
