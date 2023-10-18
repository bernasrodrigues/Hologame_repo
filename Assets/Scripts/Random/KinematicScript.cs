using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicScript : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        



    }


    private void OnCollisionExit(Collision collision)
    {

        // switch to 'non-kinematic'
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero; // or another initial value
        

    }
}
