using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagaPopUpRise : MonoBehaviour
{
    public float riseSpeed = 1f;
    public float riseTime = 1f;
    public float count;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count += Time.deltaTime;

        if (count < riseTime)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + riseSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
