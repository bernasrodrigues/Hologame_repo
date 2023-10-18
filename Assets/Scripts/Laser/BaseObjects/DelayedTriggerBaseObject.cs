using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedTriggerBaseObject : BaseObject, ITrigger
{

    public float timeTillFill;
    public float currentTimeHitByRay;


    public LevelStepTriggerCheck levelStepTriggerCheck;


    private bool detectedHit = false;





    public void Start()
    {
        levelStepTriggerCheck.triggerCompletion[this] = detectedHit;
    }



    // Start is called before the first frame update
    public override void Update()
    {
        base.Update();


        // Check if is hit, if hit increment timer else reset timer
        if (detectedHit)
        {
            currentTimeHitByRay += Time.deltaTime;
            detectedHit = false;
        }
        else
        {
            currentTimeHitByRay  = 0;
        }

        


        // Check if time to fill equals current time iluminated
        if (timeTillFill <= currentTimeHitByRay)
        {
            isHitByRay = true;
            currentTimeHitByRay = timeTillFill;
            levelStepTriggerCheck.CompleteTrigger(this);
        }
        else
        {
            isHitByRay = false;
            levelStepTriggerCheck.RemoveTrigger(this);

        }





    }

    // Update is called once per frame
    public override void HandleTouchLaser(LaserBeam laserBeam)
    {
        rayRunsOutTimer = WorldInfo.Instance.RayRunsOutTime;
        this.incomingLaserBeam = laserBeam;


        //currentTimeHitByRay += Time.deltaTime * 2;
        detectedHit = true;


    }

}
