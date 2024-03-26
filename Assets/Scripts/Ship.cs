using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ship : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTmp;
    [SerializeField] GameObject gm;
    int score;

    private void FixedUpdate()
    {
        score += 1;
        scoreTmp.SetText(score.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("gameOver");
            gm.SetActive(true);
        }
    }
}
