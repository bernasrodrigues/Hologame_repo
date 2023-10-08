using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeasurerObjectUI : MonoBehaviour
{
    public TextMeshProUGUI distanceUIText;
    public string awaitingText = "Awaiting Input";
    public float timeRemaining = 10;


    private void Start()
    {
        distanceUIText.text = awaitingText;
    }


    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            distanceUIText.text = awaitingText;
        }



    }



    public void UpdateText(string text)
    {
        distanceUIText.text = text + " m";
        timeRemaining = 10;

    }


}
