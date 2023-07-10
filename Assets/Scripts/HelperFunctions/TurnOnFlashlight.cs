using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnFlashlight : MonoBehaviour
{
    public GameObject lightsource;
    






    public void ActivateLight()
    {
        lightsource.SetActive(lightsource.activeSelf);  //flip light source (turn on/off)
    }
    

}
