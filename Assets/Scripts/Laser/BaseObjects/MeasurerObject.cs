using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurerObject : BaseObject
{
    public MeasurerObjectUI measurerObjectUI;


    public override void Update()
    {
        base.Update();


        if (incomingLaserBeam != null)
        {
            CalculateDistance();
        }



    }


    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        base.HandleTouchLaser(incomingLaserBeam);
    }


    public float CalculateDistance()
    {
        float distance = incomingLaserBeam.CalculateLaserDistance();

        measurerObjectUI.UpdateText(distance.ToString());

        return distance;
    }
}
