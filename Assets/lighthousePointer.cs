using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighthousePointer : MonoBehaviour
{
    public GameObject laserShooter;
    public GameObject target;
    public ShootLaser shootLaser;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        laserShooter.transform.LookAt(target.transform);
        laserShooter.transform.Rotate(new Vector3(90, 0, 0));


    }
}
