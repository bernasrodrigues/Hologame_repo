using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCommandCanvasAlphaAdjuster : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float timer;
    public float timeUntilFade;


    public void Update()
    {

        if (timer >= timeUntilFade)
        {
            timer = timeUntilFade;
            canvasGroup.alpha = 0;
            return;
        }

        timer += Time.deltaTime;


        float percentage = (timer / timeUntilFade);

        float alpha = Mathf.Lerp(0, 1, percentage);

        canvasGroup.alpha = (1 - alpha);

    }



    public void adjustSlider()
    {

        timer = 0;



    }




}
