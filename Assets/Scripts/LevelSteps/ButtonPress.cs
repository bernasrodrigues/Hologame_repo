using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Button m_ButtonToPress;


    void Start()
    {
        if (m_ButtonToPress == null)
            return;

        m_ButtonToPress.onClick.RemoveListener(ButtonPressHandler);
        m_ButtonToPress.onClick.AddListener(ButtonPressHandler);
    }








    void ButtonPressHandler()
    {
        LevelStepsGuide guide = GetComponentInParent<LevelStepsGuide>();

        guide.nextStep();


    }

}
