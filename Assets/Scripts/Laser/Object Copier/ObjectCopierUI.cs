using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectCopierUI : MonoBehaviour
{
    public TextMeshProUGUI objectName;
    public TextMeshProUGUI objectDescription;


    public void setText(string objectName, string objectDescription)
    {
        this.objectName.text = objectName;
        this.objectDescription.text = objectDescription;

    }






}
