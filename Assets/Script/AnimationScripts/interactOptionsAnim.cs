using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class interactOptionsAnim : MonoBehaviour
{
    float start;
    Tween anim;
    [SerializeField] float moveAmount = .1f;

    void Start()
    {
        start = transform.position.y;
    }

    void OnBecameVisible()
    {
        Debug.Log("became visible");
        transform.position = new Vector3(transform.position.x, start, 0f);
        anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);
    }

    void OnBecameInvisible()
    {
        anim.Kill();
    }
}
