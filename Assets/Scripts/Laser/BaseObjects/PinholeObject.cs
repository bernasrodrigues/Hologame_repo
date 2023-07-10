using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinholeObject : BaseObject
{
    public bool isOn = false;
    public LaserBeam incomingLaserBeam = null;
    public ShootLaser PinholeExit;


    // Update is called once per frame
    void Update()
    {
        PinholeExit.referenceLaser = incomingLaserBeam;
        PinholeExit.on = isOn;
        isOn = false;
        PinholeExit.SetPassedThroughtPinhole(true);
    }




    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        if (PinholeExit.laserBeam == incomingLaserBeam)       // prevents activating from self laser
        {
            return;
        }
    

        isOn = true;
        this.incomingLaserBeam = incomingLaserBeam;
    }


}
