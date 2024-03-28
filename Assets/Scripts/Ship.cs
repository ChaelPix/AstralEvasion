using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Ship : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTmp;
    [SerializeField] TextMeshProUGUI scoreTmp2;
    [SerializeField] GameObject explosionPrfb;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject slider;
    [SerializeField] AudioSource sound;
    [SerializeField] LeaderboardManager ld;

    int score;
    bool d;
    [HideInInspector] public bool isBoost;
    private void FixedUpdate()
    {
        score += 1;
        if (isBoost)
            score += 1;
        scoreTmp.SetText(score.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy") && !d)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (d)
            return;

        d = true;
        slider.SetActive(false);
        Debug.Log("gameOver");
        gameOver.SetActive(true);
        scoreTmp2.SetText("Score: " + score.ToString());
        Instantiate(explosionPrfb, transform.position, Quaternion.identity);
        Destroy(sound);
        ld.EndGame(score);
        Destroy(gameObject, .1f);
    }
}
