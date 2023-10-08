using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSplitter : BaseObject
{

    public List<ShootLaser> ShootLaserExits = new List<ShootLaser>();


    // Update is called once per frame
    public override void Update()
    {
        base.Update();



        foreach (ShootLaser shootLaser in ShootLaserExits)
        {
            shootLaser.referenceLaser = incomingLaserBeam;
            shootLaser.on = isHitByRay;
        }
    }





    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        foreach (ShootLaser laserBeam in ShootLaserExits)       // Check if laser colliding is not a laser that was shoot by itself
        {
            if (laserBeam.laserBeam == incomingLaserBeam)       // prevents activating from self laser
            {
                return;
            }
        }



        base.HandleTouchLaser(incomingLaserBeam);
    }



}
