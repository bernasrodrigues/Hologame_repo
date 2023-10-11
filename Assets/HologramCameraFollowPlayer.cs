using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramCameraFollowPlayer : MonoBehaviour
{

    public GameObject planeHologram;
    public GameObject viewingPoint;
    public Transform playerCamera;

    Vector3 InitialPositionOffset;
    Quaternion initialRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = WorldInfo.Instance.playerCameraTransform;


        //InitialPositionOffset = this.transform.position - playerCamera.position - planeHologram.transform.position;
        InitialPositionOffset = this.transform.position - viewingPoint.transform.position;
        //this.transform.position -= InitialOffset;

        //initialRotationOffset = Quaternion.Inverse(playerCamera.rotation) * this.transform.rotation;
        //initialRotationOffset = Quaternion.Euler(0, -90, 0); ;
        //transform.rotation = playerCamera.rotation * initialRotationOffset;







        initialRotationOffset = planeHologram.transform.rotation * playerCamera.rotation;




        print(InitialPositionOffset);
    }

    // Update is called once per frame
    void Update()
    {


        //Matrix4x4 m = placementPoint.transform.localToWorldMatrix * planeHologram.transform.localToWorldMatrix * playerCamera.transform.localToWorldMatrix;
        //this.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        //this.transform.LookAt(placementPoint.transform);

        transform.rotation = initialRotationOffset * playerCamera.transform.rotation ;


        
        //Vector3 moveVector = playerCamera.position;
        //moveVector = Vector3.Cross(playerCamera.position, initialRotationOffset.eulerAngles);
        //moveVector += InitialPositionOffset;

        //Vector3 moveVector = (playerCamera.position + InitialPositionOffset);
        //moveVector = Vector3.Cross(moveVector, Vector3.up);

        //transform.position =moveVector;

    }
}
