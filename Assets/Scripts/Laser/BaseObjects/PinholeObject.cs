using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinholeObject : BaseObject
{
    public ShootLaser PinholeExit;


    // Update is called once per frame
    public override void Update()
    {
        base.Update();


        PinholeExit.referenceLaser = incomingLaserBeam;
        PinholeExit.on = isHitByRay;
        PinholeExit.SetPassedThroughtPinhole(true);
    }




    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        if (PinholeExit.laserBeam == incomingLaserBeam)       // prevents activating from self laser
        {
            return;
        }

        base.HandleTouchLaser(incomingLaserBeam);
    }


}
