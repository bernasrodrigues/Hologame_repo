using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderObject : BaseObject
{
    public bool isOn = false;
    public LaserBeam incomingLaserBeam;        // laser Beam that touches the object
    public ShootLaser ExpanderExit;


    // Update is called once per frame
    void Update()
    {
        ExpanderExit.referenceLaser = incomingLaserBeam;
        ExpanderExit.on = isOn;
        isOn = false;

    }



    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        if (ExpanderExit.laserBeam == incomingLaserBeam)       // prevents activating from self laser
        {
            return;
        }


        isOn = true;
        this.incomingLaserBeam = incomingLaserBeam;
    }

}