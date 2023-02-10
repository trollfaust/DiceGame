[System.Serializable]
public class DiceConfig
{
    public DieFace[] faces;
    public DieFace currentFace;

    public DiceConfig(DieFace[] faces)
    {
        this.faces = faces;
        this.currentFace = faces[0];
    }
}