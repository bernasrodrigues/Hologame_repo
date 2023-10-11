using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramCameraFollowPlayer : MonoBehaviour
{

    public GameObject planeHologram;
    public GameObject placementPoint;
    public Transform playerCamera;

    Vector3 InitialOffset;
    Quaternion initialRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = WorldInfo.Instance.playerCameraTransform;


        //InitialOffset = planeHologram.transform.position - playerCamera.position;

        //this.transform.position -= InitialOffset;

        Vector3 rotationOffset = new Vector3(0, 90, 0);
        initialRotationOffset = Quaternion.Inverse(playerCamera.rotation) * transform.rotation;
        initialRotationOffset *= Quaternion.Euler(0, -90, 0); ;

        print(playerCamera.rotation);
        print(transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {


        //Matrix4x4 m = placementPoint.transform.localToWorldMatrix * planeHologram.transform.localToWorldMatrix * playerCamera.transform.localToWorldMatrix;
        //this.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        //this.transform.LookAt(placementPoint.transform);

        transform.rotation = playerCamera.rotation * initialRotationOffset;

    }
}
