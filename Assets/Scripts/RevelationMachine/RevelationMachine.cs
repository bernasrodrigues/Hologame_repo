using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevelationMachine : MonoBehaviour
{
    GoalObject goalObjectInMachine;
    public RevelationLight lightSignal;

    public float timeTillCountDown = 5f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (goalObjectInMachine)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                goalObjectInMachine.Reveal();
                lightSignal.SetMaterial(lightSignal.done);
            }
        }
    }




    private void Start()
    {
        timer = timeTillCountDown;
    }




    private void OnTriggerEnter(Collider other)
    {

        GoalObject goal = other.GetComponent<GoalObject>();

        if (goal != null)
        {

            print("QQQ");
            goalObjectInMachine = goal;
            timer = timeTillCountDown;
            lightSignal.SetMaterial(lightSignal.working);

        }
        
    }



    private void OnTriggerExit(Collider other)
    {
        GoalObject goal = other.GetComponent<GoalObject>();

        if (goal == goalObjectInMachine)
        {
            goalObjectInMachine = null;
        }

        lightSignal.SetMaterial(lightSignal.idle);
    }
}
