using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class LaserCommandPowerAdjuster : MonoBehaviour
{

    public ShootLaser shootLaser;
    public XRSlider xRslider;


    public TextMeshProUGUI textMeshPro;
    public LaserCommandCanvasAlphaAdjuster laserCommandCanvasAlphaAdjuster;



    private void Start()
    {
        shootLaser.SetPower(xRslider.value);
    }





    public void AdjustPower(float sliderValuer)
    {

        float power = Mathf.RoundToInt(Mathf.Lerp(200, 400, sliderValuer));

        shootLaser.SetPower(power);
        textMeshPro.text = power.ToString();

        laserCommandCanvasAlphaAdjuster.adjustSlider();

    }
}
