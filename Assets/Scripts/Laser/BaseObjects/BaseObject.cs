using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public bool iluminated = false;


    public ReflectiveType reflective = ReflectiveType.nonReflective;
    public float refractionIndex = 1;




    public virtual void HandleTouchLaser(LaserBeam laserBeam)
    {

    }


}
