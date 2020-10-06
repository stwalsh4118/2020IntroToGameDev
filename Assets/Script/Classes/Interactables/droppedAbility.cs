using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class droppedAbility : Interactable
{
    float start;
    Tween anim;
    [SerializeField] float moveAmount = .5f;
    SpriteRenderer sr;

    [SerializeField] Ability _droppedAbil;
    public Ability droppedAbil
    {
        get { return _droppedAbil; }
        set { _droppedAbil = value; }
    }

    protected override void Start()
    {
        base.Start();
        start = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = _droppedAbil.sprite;
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
        Debug.Log("became visible");
        transform.position = new Vector3(transform.position.x, start, 0f);
        anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Interact()
    {
        player.gameObject.GetComponentInChildren<Player>().dropAbility();
        player.gameObject.GetComponentInChildren<Player>().localPlayerData.ability = _droppedAbil;
        abilityTimer.Instance.changeAbility();
        Destroy(transform.gameObject);
    }

    public void UpdateAbility()
    {
        start = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = _droppedAbil.sprite;
    }
}
