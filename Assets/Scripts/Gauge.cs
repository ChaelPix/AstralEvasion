using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class Gauge : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image img;
    [SerializeField] int sliderSeconds;
    [SerializeField] int soundTriggerSeconds;
    [SerializeField] int soundUNTriggerSeconds;
    [SerializeField] float soundTriggerSecondsCooldown;
    [SerializeField] AudioClip[] alert;
    [SerializeField] AudioSource alertAd;
    [SerializeField] Ship ship;

    [Space]
    [SerializeField] PostProcessVolume postProcessVolume;
    private Vignette vignette;

    [SerializeField] Color startColor = Color.yellow;
    [SerializeField] Color endColor = Color.red;
    bool doSound = true;
    bool didSafe = true;

    private void Start()
    {
        slider.maxValue = sliderSeconds;
        postProcessVolume.profile.TryGetSettings(out vignette);
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
            return;
        }

        if (slider.value >= soundTriggerSeconds && doSound && didSafe)
        {
            doSound = false;
            didSafe = false;
            StartCoroutine(TempoSound());
            RandomSound();
        }

        if (slider.value <= soundUNTriggerSeconds)
            didSafe = true;

        UpdateVignetteColor();
    }

    void RandomSound()
    {
        int x = Random.Range(0, alert.Length);
        alertAd.clip = alert[x];
        alertAd.Play();
    }

    IEnumerator TempoSound()
    {
        yield return new WaitForSeconds(soundTriggerSecondsCooldown);
        doSound = true;
    }

    void UpdateVignetteColor()
    {
        if (vignette != null)
        {
            float valueRatio = slider.value / sliderSeconds;
            Color targetColor;
            if (valueRatio < 0.5f)
            {
                targetColor = Color.Lerp(Color.black, new Color(1, 0.64f, 0), valueRatio * 2);
            }
            else
            {
                targetColor = Color.Lerp(new Color(1, 0.64f, 0), Color.red, (valueRatio - 0.5f) * 2);
            }

            vignette.color.Override(targetColor);
        }
    }
}
