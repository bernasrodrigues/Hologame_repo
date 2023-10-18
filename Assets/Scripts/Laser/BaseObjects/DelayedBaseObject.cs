using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedBaseObject : BaseObject
{

    public float timeTillFill;
    public float currentTimeHitByRay;



    private bool detectedHit = false;

    // Start is called before the first frame update
    public override void Update()
    {
        base.Update();
        /*
        if (timeTillFill <= currentTimeHitByRay)
        {
            isHitByRay = true;
            currentTimeHitByRay = timeTillFill;
        }
        else
        {
            isHitByRay = false;
        }

        currentTimeHitByRay -= Time.deltaTime;

        if (currentTimeHitByRay < 0)
        {
            currentTimeHitByRay = 0;
        }
        */


        if (detectedHit)
        {
            currentTimeHitByRay += Time.deltaTime;
            detectedHit = false;
        }
        else
        {
            currentTimeHitByRay  = 0;
        }

        
        if (timeTillFill <= currentTimeHitByRay)
        {
            isHitByRay = true;
            currentTimeHitByRay = timeTillFill;
        }
        else
        {
            isHitByRay = false;
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
