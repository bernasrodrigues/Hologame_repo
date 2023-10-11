using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectCopyPlacer : MonoBehaviour
{
    //public CheckObjectInFront objectDetector; // Reference to the ObjectDetector script
    public Transform targetPosition; // The position where the copied objects will be placed

    private List<(GameObject , Vector3)> detectedObjects = new List<(GameObject, Vector3)>();
    private List<GameObject> copiedObjects = new List<GameObject>();


    public GameObject playerCamera;
    public GameObject RendererCamera;


    public CheckObjectInFront debugCheckObjectInFront;


    public static ObjectCopyPlacer Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {


    }






    public void PlaceCopy(HashSet<(GameObject, Vector3)> detectedObjects)
    {
        



        // Create copies of the detected objects
        foreach ((GameObject, Vector3) detectedObject in detectedObjects)
        {


            GameObject copy = Instantiate(detectedObject.Item1, targetPosition.position, targetPosition.rotation );


            
            try
            {
                Rigidbody rb = copy.GetComponentInChildren<Rigidbody>();
                rb.useGravity = false;
                rb.freezeRotation = true;
                rb.isKinematic = true;


            } catch (MissingComponentException)
            {
                continue;
            }





            foreach (Component component in copy.GetComponents<Component>())
            {
                // Check if it's not a MeshRenderer or MeshFilter
                if (!(component is MeshRenderer || component is MeshFilter || component is Transform))
                {
                    // Add the component to the list of components to destroy
                    DestroyImmediate(component);
                }
            }





            copy.transform.position += detectedObject.Item2;
            



            
            
            
            
            copiedObjects.Add(copy);            // not used

            //RendererCamera.transform.SetParent(playerCamera.transform.parent);
        }
    }


}