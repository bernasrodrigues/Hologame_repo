using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSelector : MonoBehaviour
{
    public Slider answerSlider;
    public int answerSelectPercentage;
    public int answerIndex;
    private float answerAdd = 0;
    public BoardController boardController;


    public void SelectAnswer()
    {
        answerAdd = 1f;
    }

    public void DeselectAnswer()
    {
        answerSlider.value = 0;
        answerAdd = 0;
        
    }


    private void Update()
    {
        print(answerAdd);
        answerSlider.value += answerAdd * Time.deltaTime;


        if (answerSlider.value >= 100)
        {
            answerSlider.value = 100;
            boardController.QuestionCheck(answerIndex);
            DeselectAnswer();
        }
    }

}
