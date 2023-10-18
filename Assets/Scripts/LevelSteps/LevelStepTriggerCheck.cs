using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStepTriggerCheck : LevelSteps
{
    public List<ITrigger> triggerList = new List<ITrigger>();

    public Dictionary<ITrigger,bool> triggerCompletion = new Dictionary<ITrigger, bool>();

    public bool levelComplete = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CompleteTrigger(ITrigger trigger)
    {
        triggerCompletion[trigger] = true;

        if (CheckTriggerCompletion())
        {
            levelComplete = true;
            print("Level complete");
        }
    }

    public void RemoveTrigger(ITrigger trigger)
    {
        triggerCompletion[trigger] = false;
    }



    bool CheckTriggerCompletion()
    {
        foreach (ITrigger t in triggerCompletion.Keys)
        {
            if (triggerCompletion[t]== false)
            {
                return false;
            }
        }

        return true;
    }
}
