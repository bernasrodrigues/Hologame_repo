using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class LaserCommandTimer : MonoBehaviour
{

    public ShootLaser shootLaser;
    public XRKnob xRknob;


    public TextMeshProUGUI tempText;
    public LaserCommandCanvasAlphaAdjuster laserCommandCanvasAlphaAdjuster;

    public TextMeshProUGUI countdownTimerText;


    private void Start()
    {
        shootLaser.RemaingTimeUpdate.AddListener(AdjustCountDownTimer);
    }




    public void AdjustTimer(float knobValue)
    {
        if (knobValue >= 0.9)
        {
            tempText.text = "Unlimited";
            countdownTimerText.text = "Unlimited";
            laserCommandCanvasAlphaAdjuster.adjustSlider();

            shootLaser.SetTimer(-1);        // Unlimited time
        }
        else
        {
            float timer = (Mathf.Lerp(0, 10, knobValue));
            timer = (float) Math.Round(timer, 2);

            tempText.text = timer.ToString();
            countdownTimerText.text = timer.ToString();


            laserCommandCanvasAlphaAdjuster.adjustSlider();
            shootLaser.SetTimer(timer);
        }


    }




    public void AdjustCountDownTimer(float timer)
    {
        timer = (float)Math.Round(timer, 2);
        tempText.text = timer.ToString();
        countdownTimerText.text = timer.ToString();

    }




}
