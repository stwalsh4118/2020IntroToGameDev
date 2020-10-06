using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class grassWaveAnim : MonoBehaviour
{
    float start = -1f;
    public float time = 2f;
    Renderer rend;
    Material mat;
    Tween anim;
    float delayRange = .5f;
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        delay = Random.Range(0, delayRange);
    }

    void OnBecameVisible()
    {
        delay = Random.Range(0, delayRange);
        Debug.Log("became visible");
        mat.SetFloat("_GrassManualAnim", start);
        anim = DOTween.To(()=> mat.GetFloat("_GrassManualAnim"), x=> mat.SetFloat("_GrassManualAnim", x), 1 ,time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
    }

    void OnBecameInvisible()
    {
        anim.Kill();
    }
}
