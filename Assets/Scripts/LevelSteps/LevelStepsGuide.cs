using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStepsGuide : MonoBehaviour
{

    public List<LevelSteps> levelSteps = new List<LevelSteps>();
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void nextStep()
    {
        levelSteps[currentIndex].EndStep();

        currentIndex += 1;

        if (currentIndex < levelSteps.Count)
        {
            levelSteps[currentIndex].BeginStep();
        }


    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
