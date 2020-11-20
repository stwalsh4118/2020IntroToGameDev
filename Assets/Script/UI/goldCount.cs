using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class goldCount : MonoBehaviour
{
    PlayerState goldNum;
    // Start is called before the first frame update
    void Start()
    {
        goldNum = GameObject.Find("PlayerObject").GetComponent<Player>().localPlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = goldNum.numGold.ToString();
    }
}
