using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class droppedWeapon : Interactable
{
    float start;
    Tween anim;
    [SerializeField] float moveAmount = .5f;
    SpriteRenderer sr;

    [SerializeField] Weapons weapon;
    public Weapons Weapon {
        get {return weapon;}
        set { weapon = value;}
    }

    protected override void Start()
    {
        base.Start();
        start = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = weapon.icon;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnBecameInvisible()
    {
        base.OnBecameInvisible();
        anim.Kill();
    }

    void OnBecameVisible()
    {
        transform.position = new Vector3(transform.position.x, start, 0f);
        anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Interact()
    {
        player.gameObject.GetComponentInChildren<Player>().localPlayerData.weapon = Weapon;
        player.gameObject.GetComponent<PlayerFire>().UpdateWeaponShotPattern();
        Destroy(transform.gameObject);
    }
}
