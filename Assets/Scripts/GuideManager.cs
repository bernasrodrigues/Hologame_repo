using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideManager : MonoBehaviour
{
    // Singleton instance
    private static GuideManager instance;
    public static GuideManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GuideManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("EventSystem");
                    instance = obj.AddComponent<GuideManager>();
                }
            }
            return instance;
        }
    }

   


    public void addEvent(LaserEvents laserEvent)
    {




    }


    public void addEvent(LaserEvents laserEvent , bool on)
    {

    }
}