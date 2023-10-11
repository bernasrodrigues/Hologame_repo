using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public Transform playerTarget;
    public Transform mirror;

    public RenderTexture mirrorTexture;
    public GameObject plane;
    public Camera mirrorCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTarget == null)
        {
            playerTarget = WorldInfo.Instance.playerCameraTransform;
        }


        //planeMeshRenderer.material.SetTexture("_MainTex", mirrorTexture);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPlayer = mirror.InverseTransformPoint(playerTarget.position);
        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, localPlayer.y, -localPlayer.z));

        Vector3 lookAtmirror = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(lookAtmirror);

        CreateViewTexture();
    }


    void CreateViewTexture()
    {
        if (mirrorTexture == null )
        {
            if (mirrorTexture != null)
            {
                mirrorTexture.Release();
            }

            mirrorTexture = new RenderTexture(Screen.width, Screen.height, 0);
            mirrorCamera.targetTexture = mirrorTexture;


            //Material mirrorMaterial = planeMeshRenderer.material;                   // creating new material
            //planeMeshRenderer.material = mirrorMaterial;

            plane.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", mirrorTexture);

        }
    }







}
