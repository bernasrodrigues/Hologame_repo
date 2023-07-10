using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootLaser : MonoBehaviour
{
    public bool on;
    public LaserBeam laserBeam;

    public ShootLaserType shootLaserType;

    // Laser References
    public LaserBeam referenceLaser;
    public LineRenderer lineRenderer;


    public float power;



    //Handle Layers
    public string[] layersItCanCollide;     // List the layers the laser can collide with




    [SerializeField] private bool loggerOn = false;
    private Logger logger;


    // Update is called once per frame
    protected virtual void Update()
    {
        if (!on)        // check if turned on, if not on ignore
        {
            if (laserBeam != null)
            {
                laserBeam.Clear();
            }
            return;
        }





        if (laserBeam != null)
        {
            laserBeam.Update(referenceLaser);
        }
        else
        {
            laserBeam = new LaserBeam(this.gameObject,
                                    referenceBeam: referenceLaser,
                                    referenceLineRenderer: lineRenderer,
                                    shootLaserType: shootLaserType,
                                    layerMask: layersItCanCollide);
            laserBeam.Update(referenceLaser);
        }


    }

    /*
    For LaserSource type
                reference laser =null
                lineRenderer

    For Object type
                reference laser 
                lineRenderer = null

     For Expander type
                reference laser 
                lineRenderer
    */


    public void button()
    {
        on = !on;
    }

    public void SetPassedThroughtPinhole(bool passed)
    {
        if (laserBeam != null) laserBeam.passedPinhole = passed;
    }
}