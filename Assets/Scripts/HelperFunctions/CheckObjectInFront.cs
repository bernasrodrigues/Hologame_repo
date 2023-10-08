using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CheckObjectInFront

public class CheckObjectInFront : MonoBehaviour
{
    public float detectionAngle = 30f; // Angle in degrees
    public float detectionRadius = 5f; // Radius within which objects will be detected

    public HashSet<(GameObject, Vector3)> detectedObjects = new HashSet<(GameObject , Vector3)>();


    private Transform parentToIgnore;
    private List<Transform> siblingToIgnore = new List<Transform>();
    public string mainObjectTag = "Object"; // Tag assigned to the PrincipalObject



    public HashSet<(GameObject, Vector3)> GetDetectedObjects()
    {
        return detectedObjects;
    }

    private void Update()
    {
        CheckFrontObjects();

        
    }



    private void Start()
    {
        // Get the parent object and its children
        parentToIgnore = transform.parent;
        if (parentToIgnore != null)
        {
            for (int i = 0; i < parentToIgnore.childCount; i++)
            {
                Transform child = parentToIgnore.GetChild(i);
                if (child != transform)
                {
                    siblingToIgnore.Add(child);
                }
            }
        }
    }




    private void CheckFrontObjects()
    {
        // Clear the list of detected objects
        detectedObjects.Clear();

        // Get all colliders within the detection radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);





        // Iterate through all the colliders
        foreach (Collider collider in colliders)
        {
            if (CheckIfDirectSibling(collider))         // Ignore direct siblings from the object (so it doesnt include itself in the copied objects)
                continue;


             // Get the BaseObject component from the collider's game object
            BaseObject baseObject = collider.gameObject.GetComponent<BaseObject>();
            if (baseObject == null)
                continue;




            if (!CheckIfObjectInFront(collider))        // Check if the object is within the angle of detection
            {
                continue;
            }



           /* if (!CheckIfIlumunatedObject(baseObject))     //TODO
            { 
                continue;
            }


            */


            // Get the root object (The one with the Object tag)
            GameObject obj = GetMainObject(collider);
            if (obj != null)
            {
                Vector3 vectorToObject = obj.transform.position - this.transform.position;
                detectedObjects.Add((obj , vectorToObject));
            }
        }
    }



    private bool CheckIfDirectSibling(Collider collider)
    {
        if (collider.gameObject == gameObject || collider.transform == parentToIgnore || siblingToIgnore.Contains(collider.transform))
            return true;


        return false;


    }


    // Skip if the collider belongs to the object running the detection or its immediate parent or siblings
    private bool CheckIfObjectInFront(Collider collider)
    {
        // Calculate the direction from this object to the collider's position
        Vector3 directionToCollider = collider.transform.position - transform.position;

        // Calculate the angle between the forward direction of this object and the direction to the collider
        float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

        // If the angle is within the detection angle, add the collider's game object to the list
        if (angleToCollider <= detectionAngle)
        {
            return true;
        }


        return false;
    }


    
    private bool CheckIfIlumunatedObject(BaseObject baseObject)
    {
        if (baseObject.isHitByRay)
        {
            return true;
        }



        return false;
    }




    // Recursivelly checks a object until it finds the parent with the object tag (the whole object at the top of hierachy contains the Object tag, where the other components are untagged)
    private GameObject GetMainObject(Collider collider)
    {
        Transform colliderToCheck = collider.transform;


        while (colliderToCheck != null)
        {
            if (colliderToCheck.CompareTag(mainObjectTag))               // Check if the collider's game object or its parents have the specified tag
            {
                return collider.transform.parent.gameObject;
            }

            colliderToCheck = colliderToCheck.parent;
        }

        return null;

    }





    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw the detection lines in the left and right directions
        Quaternion leftRotation = Quaternion.AngleAxis(-detectionAngle / 2f, transform.up) * transform.rotation;
        Quaternion rightRotation = Quaternion.AngleAxis(detectionAngle / 2f, transform.up) * transform.rotation;
        Vector3 leftDirection = leftRotation * transform.forward;
        Vector3 rightDirection = rightRotation * transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * detectionRadius);

        // Draw the detection lines in the up and down directions
        Quaternion upRotation = Quaternion.AngleAxis(-detectionAngle / 2f, transform.right) * transform.rotation;
        Quaternion downRotation = Quaternion.AngleAxis(detectionAngle / 2f, transform.right) * transform.rotation;
        Vector3 upDirection = upRotation * transform.forward;
        Vector3 downDirection = downRotation * transform.forward;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + upDirection * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + downDirection * detectionRadius);
    }
}