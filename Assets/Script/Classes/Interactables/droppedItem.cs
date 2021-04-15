using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class droppedItem : Interactable
{
    
    float start;
    Tween anim;
    [SerializeField] private float moveAmount = .3f;
    private SpriteRenderer sr;
    public Item item;
    public bool InRange = false;

    protected override void Start()
    {
        base.Start();
        start = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.icon;
        transform.position = new Vector3(transform.position.x, start, 0f);
        anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(IsWithinRange())
        {
            StopTweenInRange();
            Interact();
        }
        else
        {
            StartTweenOutOfRange();
        }
    }

    protected override void OnBecameInvisible()
    {
        base.OnBecameInvisible();
        //anim.Kill();
    }

    void OnBecameVisible()
    {
        transform.position = new Vector3(transform.position.x, start, 0f);
        //anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Interact()
    {
        Inventory.Instance.AddToInventory(item);
        Destroy(transform.gameObject);
    }

    public void StopTweenInRange()
    {
        if (anim != null)
        {
            Debug.Log("end");
            anim.Kill();
        }
    }

    public void StartTweenOutOfRange()
    {
        if (anim.IsPlaying() != true)
        {
            Debug.Log("start");
            start = transform.position.y;
            anim = transform.DOMoveY(transform.position.y + moveAmount, 1, false).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void UpdateDroppedItem()
    {
        Debug.Log("wtf");
        start = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.icon;
    }
}
