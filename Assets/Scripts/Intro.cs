using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1.0f;

    [SerializeField]
    private Transform _camera;
    [SerializeField] private float shaket = 1.0f;
    [SerializeField] private float shake = 1.0f;
    Vector3 cameraPos;

    [Space]
    [SerializeField] GameObject game;
    [SerializeField] GameObject gameCamera;
    [SerializeField] Transform sun;
    [SerializeField] float sunSize;
    [SerializeField] float sunTime;
    [SerializeField] float sunToCamTime;
    [SerializeField] float camTime;
    [SerializeField] float camToGameTime;

    [SerializeField] TMP_InputField playerNameTxt;
    [SerializeField] LeaderboardManager ld;
    private void Start()
    {
        playerNameTxt.text = PlayerSingleton.Instance.PlayerName;
        cameraPos = _camera.position;
    }

    void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        _camera.DOShakePosition(shaket, shake);
    }

    public void StartGame()
    {
        StartCoroutine(Introduction());
    }

    IEnumerator Introduction()
    {
        string _name = "snir";
        if(!string.IsNullOrWhiteSpace(playerNameTxt.text))
        {
            _name = playerNameTxt.text;
            PlayerSingleton.Instance.PlayerName = _name;
        }
        ld.Init(_name);

        _camera.DOKill();
        sun.DOScale(sunSize, sunTime);
        yield return new WaitForSeconds(sunToCamTime);

        Vector3 currentRotation = _camera.eulerAngles;
        Vector3 targetRotation = new Vector3(currentRotation.x, currentRotation.y + 180, currentRotation.z);
        _camera.DORotate(targetRotation, camTime);

        yield return new WaitForSeconds(camToGameTime/2);
        sun.gameObject.SetActive(false);
        yield return new WaitForSeconds(camToGameTime/2);

        gameObject.SetActive(false);
        gameCamera.SetActive(true);


    }
}
