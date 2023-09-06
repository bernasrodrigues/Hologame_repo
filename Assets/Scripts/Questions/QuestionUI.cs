using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    public GameObject BoardImage;

    public TMP_Text textQuestion;
    //public List<TMP_Text> questionAnswers;

    public List<GameObject> answerUIList;




    public void setQuestion(QuestionData questioData)
    {
        gameObject.SetActive(true);


        string questionText = questioData.question;
        List<string> questionAnswers = questioData.answers;        
        
        this.textQuestion.text = questionText;



        for (int i = 0; i < answerUIList.Count; i++)
        {
            TextMeshProUGUI answerText = answerUIList[i].GetComponent<TextMeshProUGUI>();
            answerText.text = questionAnswers[i];

            Slider slider = answerUIList[i].GetComponent<Slider>();
            slider.value = 0;
        }
    }

    private void cleanUp()
    {
        gameObject.SetActive(false);


        this.textQuestion.text = "";

        for (int i = 0; i < answerUIList.Count; i++)
        {
            TextMeshPro answerText = answerUIList[i].GetComponent<TextMeshPro>();
            answerText.text = "";

            Slider slider = answerUIList[i].GetComponent<Slider>();
            slider.value = 0;
        }
    }

}
