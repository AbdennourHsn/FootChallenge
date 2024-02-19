using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    public Image image;

    void Start()
    {
        this.image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        FadeOut(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn(float timing )
    {
        image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 1f), timing);
    }

    public void FadeOut(float timing)
    {
        image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0f), timing);
    }

    public void PingPong()
    {
        StartCoroutine(FadePP());
    }

    private IEnumerator FadePP()
    {
        FadeIn(0.5f);
        yield return new WaitForSeconds(0.6f);
        FadeOut(1f);

    }
}
