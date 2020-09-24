using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomAnimationSpeed : MonoBehaviour
{
    public float speedRange = 1f;
    public float animSpeed = 0;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        animSpeed = Random.Range(.5f, speedRange);
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = animSpeed;
    }
}
