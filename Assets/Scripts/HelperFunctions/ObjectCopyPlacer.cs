using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCopyPlacer : MonoBehaviour
{
    //public CheckObjectInFront objectDetector; // Reference to the ObjectDetector script
    public Transform targetPosition; // The position where the copied objects will be placed

    private List<(GameObject , Vector3)> detectedObjects = new List<(GameObject, Vector3)>();
    private List<GameObject> copiedObjects = new List<GameObject>();


    public GameObject playerCamera;
    public GameObject RendererCamera;




    public CheckObjectInFront debugCheckObjectInFront;



    private void Start()
    {
        /*
        // Get the detected objects from the ObjectDetector script
        if (objectDetector != null)
        {
            detectedObjects = objectDetector.GetDetectedObjects();
        }

        */
    }


    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlaceCopy(debugCheckObjectInFront.detectedObjects);
        };


    }






    public void PlaceCopy(HashSet<(GameObject, Vector3)> detectedObjects)
    {
        



        // Create copies of the detected objects
        foreach ((GameObject, Vector3) detectedObject in detectedObjects)
        {
            GameObject copy = Instantiate(detectedObject.Item1.gameObject, targetPosition.position, targetPosition.rotation );



            try
            {
                Rigidbody rb = copy.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.freezeRotation = true;
                rb.isKinematic = true;


            } catch (MissingComponentException)
            {
                continue;
            }


            copy.transform.position += detectedObject.Item2;
            copiedObjects.Add(copy);

            //RendererCamera.transform.SetParent(playerCamera.transform.parent);
        }
    }


}