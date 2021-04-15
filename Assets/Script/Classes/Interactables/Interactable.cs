using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public float radius = 1.5f;
    protected Transform player;
    protected bool isInteracting = false;
    public bool disappearOnExitScreen = false;
    protected float distance;

    protected bool promptRaised = false;

    protected virtual void Start()
    {
        player = GameObject.Find("Character").transform;
    }
    public virtual void Interact()
    {
        // This method is meant to be overwritten by other classes.
        Debug.Log("Interacting with " + transform.name);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected virtual void Update()
    {

        distance = Vector3.Distance(player.position, transform.position);

        if (IsWithinRange() && !isInteracting)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    protected virtual void OnBecameInvisible()
    {
        if (disappearOnExitScreen)
        {
            transform.gameObject.SetActive(false);
        }
    }

    protected bool IsWithinRange()
    {
        if (distance <= radius)
        {
            return true;
        }
        else return false;
    }
}

