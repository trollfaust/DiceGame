public class Quest
{
    public string title;
    public string effectText;
    public SlotConfig[] slots;
    public delegate void OnComplete();
    OnComplete myOnComplete;
    public QUESTDIFFICULTY difficulty;

    public Quest(string title, string effectText, QUESTDIFFICULTY difficulty, SlotConfig[] slots, OnComplete onComplete)
    {
        this.title = title;
        this.effectText = effectText;
        this.slots = slots;
        this.myOnComplete = onComplete;
        this.difficulty = difficulty;
    }

    public void Complete()
    {
        myOnComplete();
    }
}
