using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevelationLight : MonoBehaviour
{
    public Material idle;
    public Material working;
    public Material done;





    public void SetMaterial(Material mat)
    {

        Renderer rend = GetComponent<Renderer>();

        rend.material = mat;

    }
}
