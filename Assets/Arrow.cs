using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    public void Setup(SlotConfig slotConfig)
    {
        int index = Mathf.Clamp(Mathf.Abs(slotConfig.changeAmount) - 1, 0, sprites.Length - 1);
        image.sprite = sprites[index];
    }
}
