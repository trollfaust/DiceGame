using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public Animator animator;
    public Image[] faceImages;
    public float tooltipDelay = 3f;

    private Coroutine coroutine;

    public void Setup(DiceConfig diceConfig)
    {
        for (int i = 0; i < diceConfig.faces.Length; i++)
        {
            faceImages[i].sprite = diceConfig.faces[i].sprite;
        }
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Delay(tooltipDelay));
    }

    IEnumerator Delay(float tooltipDelay)
    {
        animator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(tooltipDelay);
        animator.SetBool("FadeIn", false);
    }

}
