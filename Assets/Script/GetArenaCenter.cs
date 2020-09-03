using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArenaCenter : MonoBehaviour
{
    public static GetArenaCenter centerCoordinates;
    public float x;
    public float y;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        centerCoordinates = this;
        x = transform.position.x;
        y = transform.position.y;
    }

}
