using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStepTriggerCheck : LevelSteps
{
    Trigger hitChecker;
    public List<Trigger> triggerList = new List<Trigger>();

    public Dictionary<Trigger,bool> triggerCompletion = new Dictionary<Trigger, bool>();


    public LevelStepsGuide levelStepsGuide;



    // Start is called before the first frame update
    void Start()
    {
        foreach (Trigger t in triggerList)
        {
            t.completeTrigger.AddListener(CompleteTrigger);
            t.removeTrigger.AddListener(RemoveTrigger);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void CompleteTrigger(Trigger trigger)
    {
        levelStepsGuide.nextStep();

        

    }

    void RemoveTrigger(Trigger trigger)
    {
        //TODO
    }
}
