using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityTimer : MonoBehaviour
{

    public Slider slider;
    public float abilityTime;
    public float count = 0;
    Player player;
    Image abilitySprite;
    public static abilityTimer Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        player = FindObjectOfType<Player>();

        
        if (player.localPlayerData.ability == null)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            foreach (Transform child in transform)
            {
                if (child.name == "AbilitySprite")
                {
                    abilitySprite = child.GetComponent<Image>();
                }
            }

            abilitySprite.sprite = player.localPlayerData.ability.sprite;
            abilityTime = player.localPlayerData.ability.abilityTime;
            slider.maxValue = abilityTime;
            slider.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.localPlayerData.ability == null)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            if (count > abilityTime)
            {
                if (Input.GetKeyDown("q"))
                {
                    slider.value = 0;
                    count = 0;
                    StartCoroutine(player.localPlayerData.ability.activateAbility());
                }
            }
            else
            {
                count += Time.deltaTime;
                slider.value = count;
            }
        }

    }

    public void changeAbility()
    {
        abilityTime = player.localPlayerData.ability.abilityTime;
        slider.maxValue = abilityTime;
        slider.value = 0;
        count = 0;

        foreach (Transform child in transform)
        {
            if (child.name == "AbilitySprite")
            {
                abilitySprite = child.GetComponent<Image>();
            }
        }
        abilitySprite.sprite = player.localPlayerData.ability.sprite;
    }

}
