using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownManager : MonoBehaviour
{
    public List<GameObject> AllCrowns;
    public List<GameObject> AquiredCrowns;
    // Start is called before the first frame update
    void Start()
    {
        AquiredCrowns = new List<GameObject>();
        foreach(GameObject crown in AllCrowns ) {
            AquiredCrowns.Add(crown);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
