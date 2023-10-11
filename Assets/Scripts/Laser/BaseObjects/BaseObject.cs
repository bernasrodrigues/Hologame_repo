using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public bool isHitByRay = false;
    public float rayRunsOutTimer;
    public LaserBeam incomingLaserBeam;        // laser Beam that touches the object

    public ObjectInfo objectInfo;



    public ReflectiveType reflective = ReflectiveType.nonReflective;
    public float refractionIndex = 1;



    public virtual void Update()
    {
        rayRunsOutTimer -= Time.deltaTime;

        if (rayRunsOutTimer <= 0)   //check if time run out, if it has, no longer being hit by ray
        {
            isHitByRay = false;
            rayRunsOutTimer = 0;
            incomingLaserBeam = null;
        }
    }



    // if hit toggle being hit and set timer until not iluminated
    public virtual void HandleTouchLaser(LaserBeam laserBeam)
    {
        isHitByRay = true;
        rayRunsOutTimer = WorldInfo.Instance.RayRunsOutTime;
        this.incomingLaserBeam = laserBeam;

    }


}
