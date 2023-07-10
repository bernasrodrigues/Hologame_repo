using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableAdjuster : MonoBehaviour
{
    public Transform TableTransform;
    public float maxHeightAdjustment;
    public float startHeight;
    public float smoothness;

    private float minHeight;
    private float maxHeight;
    private bool _hasStarted = false;


    private Vector3 newPosition;
    private Vector3 velocity;        // Current velocity


    // Start is called before the first frame update
    void Start()
    {
        startHeight = TableTransform.position.y;

        minHeight = startHeight - maxHeightAdjustment;
        maxHeight = startHeight + maxHeightAdjustment;


        newPosition = this.transform.position;          // new position starts as the original position, if not defined the object tries to move to (0,0,0)

        _hasStarted = true;




    }

    // Update is called once per frame
    void Update()
    {


        Vector3 smoothedPosition = Vector3.SmoothDamp(this.transform.position, newPosition, ref velocity , smoothness);
        this.transform.position = smoothedPosition;


    }




    public void HoloTableAdjuster(float handleValue)
    {
        if (!_hasStarted)
        {
            return;
        }


        float height = Mathf.Lerp(minHeight, maxHeight , handleValue );
        newPosition = new Vector3(this.transform.position.x, height, this.transform.position.z);

    }

}
