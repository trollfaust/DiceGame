using UnityEngine;

public enum FACECOLOR { none, blue, red, swap }

public class SlotConfig
{
    public int minValue;
    public FACECOLOR color;
    public int changeAmount;
    public FACECOLOR changeColor;

    public SlotConfig(int minValue, FACECOLOR color, int changeAmount, FACECOLOR changeColor)
    {
        this.minValue = minValue;
        this.color = color;
        this.changeAmount = changeAmount;
        this.changeColor = changeColor;
    }

    public SlotConfig(int minValue, FACECOLOR color, int changeAmount) : this(minValue, color, changeAmount, FACECOLOR.none) { }
    public SlotConfig(int minValue, FACECOLOR color) : this(minValue, color, 0) { }
    public SlotConfig(int minValue) : this(minValue, FACECOLOR.none) { }
    public SlotConfig() : this(0) { }
}
