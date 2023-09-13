using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : BaseObject
{
    public bool isTouching;
    public float timeOut = 0.3f;
    public float timeOutCount;
    public float timeTillFull = 3f;
    public float timeCount;

    
    public ObjectCopyPlacer objectCopyPlacer;
    public bool createdHologram = false;

    public CheckObjectInFront objectDetector;


    public GameObject virtualPlane;


    public override void HandleTouchLaser(LaserBeam laserBeam)
    {
        isTouching = true;
    }



    public void Update()
    {
        CheckTimeCountDown();
        CheckCreateCopy();


        isTouching = false;

    }


    public void CheckTimeCountDown()
    {
        if (isTouching)
        {
            timeCount = Math.Min(timeCount + Time.deltaTime, timeTillFull);             // what value is smaller timetillfull or the timecount (no point in making the time count larger than time full)
        }
        else
        {
            timeOutCount = Math.Min(timeOutCount + Time.deltaTime, timeOut);
            if (timeOutCount == timeOut)
            {
                timeCount = 0;
                timeOutCount = 0;
            }
        }


    }


    public void CheckCreateCopy()
    {
        if (timeCount >= timeTillFull && !createdHologram)      // check if the timer has reached full and has not yet created a hologram
        {
            createdHologram = true;
            objectCopyPlacer.PlaceCopy(objectDetector.detectedObjects);
        }




    }


    public void Reveal()
    {
        virtualPlane.gameObject.SetActive(true);


    }





}
