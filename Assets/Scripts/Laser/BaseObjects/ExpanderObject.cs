using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderObject : BaseObject
{
    public ShootLaser ExpanderExit;


    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        ExpanderExit.referenceLaser = incomingLaserBeam;
        ExpanderExit.on = isHitByRay;

    }




    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        if (ExpanderExit.laserBeam == incomingLaserBeam)       // prevents activating from self laser
        {
            return;
        }



        base.HandleTouchLaser(incomingLaserBeam);
    }

}