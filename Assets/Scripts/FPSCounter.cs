using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;

    [SerializeField] TextMeshProUGUI tmp;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        tmp.SetText(fps.ToString("000") + " fps");
    }
}