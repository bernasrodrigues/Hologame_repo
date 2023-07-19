using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionIgnorer : MonoBehaviour
{
    public Collider mirroHolderCollider;
    public GameObject startIgnore;

    public void Start()
    {
        XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();

        socket.onSelectEntered.AddListener(SocketEnter);
        socket.onSelectExited.AddListener(SocketExit);


        if (startIgnore != null)
        {
            Collider[] colliders = startIgnore.GetComponentsInChildren<Collider>();
            foreach (Collider c in colliders)
            {
                Physics.IgnoreCollision(c, mirroHolderCollider, true);
            }
        }

    }


    public void SocketEnter(XRBaseInteractable obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            Physics.IgnoreCollision(c, mirroHolderCollider, true);
        }

    }

    public void SocketExit(XRBaseInteractable obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            Physics.IgnoreCollision(c, mirroHolderCollider, false);
        }
    }
}
