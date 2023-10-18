using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckObjectsHitByRayTrigger : Trigger
{
    public List<BaseObject> objectiveList = new List<BaseObject>();

    //public UnityEvent completeSignal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CheckObjectivesHit())
        {
            completeTrigger.Invoke(this);
        }

    }



    bool CheckObjectivesHit()
    {

        foreach (BaseObject objective in objectiveList)
        {
            if (!objective.isHitByRay)
            {
                return false;
            }
        }

        return true;
    }
}
