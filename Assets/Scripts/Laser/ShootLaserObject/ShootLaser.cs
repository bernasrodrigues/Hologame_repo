using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public LaserEvents LaserEventType;

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

    public void button()
    {
        on = !on;

        GuideManager.Instance.addEvent(LaserEventType, on);        
    }

    public void SetPassedThroughtPinhole(bool passed)
    {
        if (laserBeam != null) laserBeam.passedPinhole = passed;
    }
}