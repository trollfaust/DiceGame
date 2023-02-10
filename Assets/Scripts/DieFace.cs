using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Die Face", fileName = "New Die Face")]
public class DieFace : ScriptableObject
{
    public int value;
    public Sprite sprite;
    public FACECOLOR color;
}
