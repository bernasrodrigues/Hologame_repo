using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSteps : MonoBehaviour
{

    // Start is called before the first frame update
    public void EndStep()
    {
        this.gameObject.SetActive(false);


    }

    // Update is called once per frame
    public void BeginStep()
    {
        this.gameObject.SetActive(true);



    }
}
