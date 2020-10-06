using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class damagePopUpRise : MonoBehaviour
{
    public float riseDistance = 1f;
    public float riseTime = 1f;
    public float count;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.position.y + riseDistance, riseTime, false)
        .OnComplete(DestroyThis);
    }

    void DestroyThis() {
        Destroy(transform.gameObject);
    }
}
