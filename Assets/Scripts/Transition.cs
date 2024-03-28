using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] float posY;
    [SerializeField] float time;

    private void Start()
    {
        transform.DOMoveY(posY, time).OnComplete(() => Destroy(gameObject));
    }
}
