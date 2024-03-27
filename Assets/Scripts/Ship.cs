using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ship : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTmp;
    [SerializeField] TextMeshProUGUI scoreTmp2;
    [SerializeField] GameObject explosionPrfb;
    [SerializeField] GameObject gameOver;
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
            gameOver.SetActive(true);
            scoreTmp2.SetText("Score: " + score.ToString());
            Instantiate(explosionPrfb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
