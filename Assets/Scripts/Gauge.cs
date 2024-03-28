using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image img;
    [SerializeField] int sliderSeconds;

    [SerializeField] Ship ship;

    [SerializeField] Color startColor = Color.yellow;
    [SerializeField] Color endColor = Color.red;
    private void Start()
    {
        slider.maxValue = sliderSeconds;
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            slider.value -= Time.deltaTime;
        }
        else
        {
            slider.value += Time.deltaTime;
        }
        img.color = Color.Lerp(startColor, endColor, slider.value / sliderSeconds);

        if (slider.value >= sliderSeconds)
        {
            ship.GameOver();
            this.enabled = false;
        }
        
    }
}
