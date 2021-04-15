using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public AudioClip a;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.LoopMusic(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
