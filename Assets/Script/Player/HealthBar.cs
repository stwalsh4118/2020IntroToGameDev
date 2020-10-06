using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar playerHP;
    public Transform player;
    public int maxHealth;
    public int startingHealth;
    public string baseHPType;
    public float totalHealthValue = 0;
    public GameObject deathOverlay;
    

    [System.Serializable]
    public class HealthUnitTypes
    {
        public GameObject HPType;
    }
    public List<HealthUnitTypes> HPTYPES;
    public List<GameObject> HPBar;

    void Awake()
    {
        playerHP = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        HPBar = new List<GameObject>();
        player = GameObject.FindObjectOfType<Player>().transform.parent.transform;
        InstantiateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        float removedHealthUnitValue = 0;
        if (totalHealthValue > 0)
        {
            GameObject hpEnd = HPBar.Last();
            string AHT = hpEnd.GetComponent<HealthUnit>().addHealthType;
            if (AHT == "")
            {
                HPBar.Remove(hpEnd);
                string takeDamageType = hpEnd.GetComponent<HealthUnit>().takeDamageType;
                Debug.Log(takeDamageType);
                foreach (HealthUnitTypes HUT in HPTYPES)
                {
                    if (HUT.HPType.GetComponent<HealthUnit>().type == takeDamageType)
                    {
                        removedHealthUnitValue = HUT.HPType.GetComponent<HealthUnit>().healthValue;
                        Debug.Log(HUT.HPType.GetComponent<HealthUnit>().type);
                        GameObject newHPType = (GameObject)Instantiate(HUT.HPType, transform);
                        HPBar.Add(newHPType);
                    }
                }
                totalHealthValue -= (hpEnd.GetComponent<HealthUnit>().healthValue - removedHealthUnitValue);
                hpEnd.SetActive(false);
            }
            else
            {
                int count = 0;
                int takeHPFromIndex = 0;
                foreach (GameObject HU in HPBar)
                {
                    if (HU.GetComponent<HealthUnit>().type == AHT)
                    {
                        takeHPFromIndex = count;
                    }
                    count++;
                }
                Debug.Log(takeHPFromIndex);

                GameObject takeHPFrom = HPBar[takeHPFromIndex];
                string takeDamageType = takeHPFrom.GetComponent<HealthUnit>().takeDamageType;
                Debug.Log(takeDamageType);
                foreach (HealthUnitTypes HUT in HPTYPES)
                {
                    if (HUT.HPType.GetComponent<HealthUnit>().type == takeDamageType)
                    {
                        removedHealthUnitValue = HUT.HPType.GetComponent<HealthUnit>().healthValue;
                        Debug.Log(HUT.HPType.GetComponent<HealthUnit>().type);
                        GameObject newHPType = (GameObject)Instantiate(HUT.HPType, transform);
                        HPBar[takeHPFromIndex] = newHPType;
                    }
                }
                totalHealthValue -= (takeHPFrom.GetComponent<HealthUnit>().healthValue - removedHealthUnitValue);
                takeHPFrom.SetActive(false);

            }
            if (totalHealthValue <= 0)
            {
                PlayerDie();
            }
            PlaceHealthOnScreen();
        }
        else
        {
            PlayerDie();
        }
    }

    public void InstantiateHealthBar()
    {
        foreach(HealthUnitTypes HUT in HPTYPES)
        {
            if (HUT.HPType.GetComponent<HealthUnit>().type == baseHPType)
            {
                for(int i = 0; i < startingHealth; i++)
                {
                    GameObject baseHP = (GameObject)Instantiate(HUT.HPType, transform);
                    totalHealthValue += baseHP.GetComponent<HealthUnit>().healthValue;
                    HPBar.Add(baseHP);
                }
            }
        }
        PlaceHealthOnScreen();
    }

    public void PlaceHealthOnScreen()
    {
        for (int index = 0; index < HPBar.Count(); index++)
        {
            HPBar[index].transform.localPosition = new Vector3((float)index/2, 0f, 0f);
        }

    }

    public void PlayerDie()
    {
        player.gameObject.SetActive(false);
        deathOverlay.SetActive(true);
    }
}
