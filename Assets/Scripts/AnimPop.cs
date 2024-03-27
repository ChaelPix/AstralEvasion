using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimPop : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float size;

    private void Start()
    {
        transform.DOScale(size, time).From(1f).SetLoops(-1, LoopType.Yoyo);
    }
}
