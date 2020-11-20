using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class goldDropInteractable : Interactable
{
    public int gold;
    public bool collected = false;
    public AudioClip coinSound;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(IsWithinRange())
        {
            if (!collected)
            {
                Interact();
                collected = true;
            }
        }
    }

    public override void Interact()
    {
        GetComponent<Animator>().SetTrigger("OnDeath");
        StartCoroutine(goldCollectionAnim());
        SoundManager.Instance.Play(coinSound);

    }

    public IEnumerator goldCollectionAnim()
    {
        Debug.Log(gold);
        int dividedGold = (gold / 10);
        int goldRemainder = gold - (dividedGold * 10);
        for (int i = 0; i < 10; i++)
        {
            GameObject.Find("PlayerObject").GetComponent<Player>().localPlayerData.numGold += dividedGold;
            Vector2 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 rectPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("HUD").GetComponent<RectTransform>(), sp, Camera.main, out rectPoint);
            rectPoint.x += Random.Range(-50, 50);
            rectPoint.y += Random.Range(-50, 50);
            rectPoint.x -= 600f;
            rectPoint.y -= 336f;
            GameObject g = Instantiate(Resources.Load("Prefabs/GoldCoin", typeof(GameObject)), GameObject.Find("HUD").transform) as GameObject;
            g.GetComponent<RectTransform>().anchoredPosition = rectPoint;
            Vector2 goldUIPosition = GameObject.Find("Gold").GetComponent<RectTransform>().anchoredPosition;
            g.GetComponent<RectTransform>().DOAnchorPos(goldUIPosition, 1, false).OnComplete(() => Destroy(g));
            yield return new WaitForSeconds(.025f);
        }
        GameObject.Find("PlayerObject").GetComponent<Player>().localPlayerData.numGold += goldRemainder;

        Destroy(gameObject);
    }
}
