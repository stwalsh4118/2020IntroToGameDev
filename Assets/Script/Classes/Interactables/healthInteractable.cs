using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class healthInteractable : Interactable
{
    public string healthType;
    public float moveAmount = .3f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Tween anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character")
        {
            if ((healthType == HealthBar.playerHP.baseHPType) && (HealthBar.playerHP.numBaseHP < HealthBar.playerHP.maxHealth))
            {
                HealthBar.playerHP.GainHealth(healthType);
                Destroy(gameObject);
            }
            else if(healthType != HealthBar.playerHP.baseHPType)
            {
                HealthBar.playerHP.GainHealth(healthType);
                Destroy(gameObject);
            }
        }
    }
}
