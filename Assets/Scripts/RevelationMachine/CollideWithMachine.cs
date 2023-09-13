using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithMachine : MonoBehaviour
{
    public GameObject objectToAffect;

    private void Start()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != objectToAffect)
        {
            foreach (Collider col in collision.gameObject.GetComponents<Collider>())
            {
                Physics.IgnoreCollision(col, this.gameObject.GetComponent<Collider>(), true);

            }
        }
    }
}
