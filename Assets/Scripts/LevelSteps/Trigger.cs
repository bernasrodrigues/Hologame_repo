using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{
    public UnityEvent<Trigger> completeTrigger;
    public UnityEvent<Trigger> removeTrigger;
}
