using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShootLaser : MonoBehaviour
{
    public bool on;
    public LaserBeam laserBeam;

    public ShootLaserType shootLaserType;

    // Laser References
    public LaserBeam referenceLaser;
    public LineRenderer lineRenderer;

    public float power;
    public float timer = -1;


    public UnityEvent<float> RemaingTimeUpdate;


    //Handle Layers
    public string[] layersItCanCollide;     // List the layers the laser can collide with

    public LaserEvents LaserEventType;


    // Update is called once per frame
    protected virtual void Update()
    {


        if (!on)        // check if turned on, if not on ignore
        {
            if (laserBeam != null)
            {
                laserBeam.Clear();
            }
            return;
        }





        if (laserBeam != null)
        {
            laserBeam.Update(referenceLaser);
        }
        else
        {
            laserBeam = new LaserBeam(this.gameObject,
                                    referenceBeam: referenceLaser,
                                    referenceLineRenderer: lineRenderer,
                                    shootLaserType: shootLaserType,
                                    layerMask: layersItCanCollide,
                                    power: power);
            laserBeam.Update(referenceLaser);
        }


    }

    public void button()
    {
        on = !on;

        if (on)
        {
            StartTimer();
        }

    }

    public void SetPassedThroughtPinhole(bool passed)
    {
        //if (laserBeam != null) laserBeam.passedPinhole = passed;
    }



    public void SetPower(float power)
    {
        this.power = power;
    }



    public void SetTimer(float timer)
    {
        this.timer = timer;
    }


    
    public void StartTimer()
    {
        if (timer > 0)
        {
            StartCoroutine(CountDownTimer());
        }
    }

    public IEnumerator CountDownTimer()
    {
        float remainingTime = timer;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            RemaingTimeUpdate.Invoke(remainingTime);

            yield return null;
        }


        on = false;
    }
    
}