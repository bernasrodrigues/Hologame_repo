using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEventHandler : MonoBehaviour
{
    public LaserEvents LaserEventsType;


    public void sendEvent(bool on)
    {
        GuideManager.Instance.addEvent(LaserEventsType , on);
    }

}


public enum LaserEvents
{
    None,
    TurnOn,
    BeamSplitter,
    Expander,
    ObjectIluminated,
    PlateIlumminated,

}