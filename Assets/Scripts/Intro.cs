using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private void Start()
    {
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
