using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeMaster : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] GameObject band;

    private void Start()
    {
        band.SetActive(!PlayerSingleton.Instance.hasSound);
    }
    public void SwitchVolume()
    {
        bool b = !PlayerSingleton.Instance.hasSound;
        masterMixer.SetFloat("Volume", !b ? -80f : -0f);
        PlayerSingleton.Instance.hasSound = b;
        band.SetActive(!b);
    }
}
