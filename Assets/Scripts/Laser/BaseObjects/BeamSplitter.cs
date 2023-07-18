using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSplitter : BaseObject
{

    public bool isOn = false;
    public LaserBeam incomingLaserBeam = null;
    public List<ShootLaser> ShootLaserExits = new List<ShootLaser>();


    public LaserEvents LaserEventType;


    // Update is called once per frame
    void Update()
    {
        foreach (ShootLaser shootLaser in ShootLaserExits)
        {
            //shootLaser.setLaser(incomingLaserBeam);
            shootLaser.referenceLaser = incomingLaserBeam;
            shootLaser.on = isOn;


        }
        GuideManager.Instance.addEvent(LaserEventType, isOn);

        isOn = false;


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

        isOn = true;
        this.incomingLaserBeam = incomingLaserBeam;
    }

}
