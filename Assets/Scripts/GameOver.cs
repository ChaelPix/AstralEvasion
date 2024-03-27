using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] Transform gmPanel;
    [SerializeField] Transform scoreTmp;
    [SerializeField] Transform buttonTmp;
    [SerializeField] Transform TransitionPanel;

    [SerializeField] float timeToAppearGM;
    private void Start()
    {
        StartCoroutine(GameOverAnim());
    }


    IEnumerator GameOverAnim()
    {
        gmPanel.gameObject.SetActive(true);
        gmPanel.DOScale(1.1f, timeToAppearGM).From(0).OnComplete(() => gmPanel.DOScale(1, .1f));
        yield return new WaitForSeconds(timeToAppearGM / 2);

        scoreTmp.gameObject.SetActive(true);
        scoreTmp.DOScale(1.1f, timeToAppearGM).From(0).OnComplete(() => scoreTmp.DOScale(1, .1f));
        yield return new WaitForSeconds(timeToAppearGM / 2);

        buttonTmp.gameObject.SetActive(true);
        buttonTmp.DOScale(1.1f, timeToAppearGM).From(0).OnComplete(() => buttonTmp.DOScale(1, .1f));
        yield return new WaitForSeconds(timeToAppearGM / 2);
    }

    public void Restart()
    {
        TransitionPanel.gameObject.SetActive(true);
        TransitionPanel.DOMoveY(.8f, .5f).OnComplete(() => SceneManager.LoadScene(0));
       
    }
}
