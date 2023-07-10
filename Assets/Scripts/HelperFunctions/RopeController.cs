using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class RopeController : MonoBehaviour
{
    public int segmentCount = 10;   // Number of segments in the rope
    public float segmentLength = 0.2f;   // Length of each segment

    public GameObject ropeSegmentPrefab;   // Prefab for the rope segment

    private void Start()
    {
        CreateRope();
    }

    private void CreateRope()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.GetChild(0).position;

        Vector3 ropeDirection = (endPosition - startPosition).normalized;
        float segmentSpacing = segmentLength / segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 segmentPosition = startPosition + ropeDirection * (i * segmentSpacing);
            GameObject segment = Instantiate(ropeSegmentPrefab, segmentPosition, Quaternion.identity);
            segment.transform.SetParent(transform);

            if (i == segmentCount - 1)
            {
                // Connect the end segment to the end point GameObject
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = transform.GetChild(0).GetComponent<Rigidbody>();
            }
            else
            {
                // Connect the segments together
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = transform.GetChild(i + 1).GetComponent<Rigidbody>();
            }
        }
    }
}