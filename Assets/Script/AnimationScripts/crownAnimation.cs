using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class crownAnimation : MonoBehaviour
{
    Sequence crownAnim;
    Tween camAnim;
    CinemachineVirtualCamera vcam;
    private float animLength = 1f;
    private float radius = 3f;
    private float angle = 0f;
    private float angleIncrement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("g"))
            {

            }
            else
            {
                if (crownAnim != null)
                {
                    crownAnim.Complete(true);
                }
            }

        }
        if (Input.GetKeyDown("g"))
        {
            CrownAnimation();
        }
    }

    public void CrownAnimation()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        angleIncrement = 360f / GetComponent<CrownManager>().AquiredCrowns.Count;
        Debug.Log(angleIncrement);
        Debug.Log(GetComponent<CrownManager>().AquiredCrowns.Count);
        int index = 0;
        crownAnim = DOTween.Sequence();
        camAnim = DOTween.To(()=> vcam.m_Lens.OrthographicSize, x=> vcam.m_Lens.OrthographicSize = x, 4, 1);
        crownAnim.Insert(0, camAnim);
        foreach (GameObject crown in GetComponent<CrownManager>().AquiredCrowns)
        {

            GameObject animatedCrown = (GameObject)Instantiate(crown);
            animatedCrown.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            crownAnim.Insert(.3f * index, animatedCrown.transform.DOLocalMove(new Vector3(animatedCrown.transform.position.x + (radius * Mathf.Cos(angle * Mathf.Deg2Rad)), animatedCrown.transform.position.y + (radius * Mathf.Sin(angle * Mathf.Deg2Rad)), 0f), animLength, false));
            index++;
            Debug.Log(angle);
            angle += angleIncrement;
        }
        crownAnim.AppendInterval(1f);
        crownAnim.AppendCallback(DestroyCrown);
        angle = 0;
    }

    void DestroyCrown()
    {
        Debug.Log("Start Destroying");
        foreach (Crown child in GameObject.FindObjectsOfType<Crown>())
        {
            Debug.Log("Destroyed");
            Destroy(child.transform.gameObject);
        }
        
        camAnim = DOTween.To(()=> vcam.m_Lens.OrthographicSize, x=> vcam.m_Lens.OrthographicSize = x, 8, 1);
    }
}
