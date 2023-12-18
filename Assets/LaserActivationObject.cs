using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserActivationObject : BaseObject
{
    public ShootLaser LaserExit;


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        LaserExit.on = isHitByRay;

    }




    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        if (LaserExit.laserBeam == incomingLaserBeam)       // prevents activating from self laser
        {
            return;
        }



        base.HandleTouchLaser(incomingLaserBeam);
    }

}