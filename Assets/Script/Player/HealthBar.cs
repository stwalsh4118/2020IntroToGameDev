using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar playerHP;
    public Transform player;
    public PlayerState lpd;
    public int maxHealth;
    public int startingHealth;
    public string baseHPType;
    public int numBaseHP;
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
        lpd = GameObject.FindObjectOfType<Player>().localPlayerData;
        InstantiateHealthBar();
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
                lpd.health.Remove(lpd.health.Last());
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
                        lpd.health.Add(newHPType.GetComponent<HealthUnit>().type);
                    }
                }
                totalHealthValue -= (hpEnd.GetComponent<HealthUnit>().healthValue - removedHealthUnitValue);
                if(hpEnd.GetComponent<HealthUnit>().type == baseHPType)
                {
                    numBaseHP--;
                }
                Destroy(hpEnd);
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
                        lpd.health[takeHPFromIndex] = newHPType.GetComponent<HealthUnit>().type;
                    }
                }
                totalHealthValue -= (takeHPFrom.GetComponent<HealthUnit>().healthValue - removedHealthUnitValue);
                if(takeHPFrom.GetComponent<HealthUnit>().type == baseHPType)
                {
                    numBaseHP--;
                }
                Destroy(takeHPFrom);

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

    public void GainHealth(string HPGained)
    {

        string hpPath = "Prefabs/Health/" + HPGained;
        if (HPBar.Exists(x => x.GetComponent<HealthUnit>().addHealthType == HPGained))
        {
            int hpToSwap = HPBar.FindIndex(x => x.GetComponent<HealthUnit>().addHealthType == HPGained);
            GameObject removedHU = HPBar[hpToSwap];
            GameObject hpg = (GameObject)Instantiate(Resources.Load(hpPath, typeof(GameObject)), transform) as GameObject;
            HPBar[hpToSwap] = hpg;
            lpd.health[hpToSwap] = HPGained;
            totalHealthValue += hpg.GetComponent<HealthUnit>().healthValue;
            Destroy(removedHU);
        }
        else
        {
            if (HPGained != baseHPType)
            {
                GameObject hpg = (GameObject)Instantiate(Resources.Load(hpPath, typeof(GameObject)), transform) as GameObject;
                totalHealthValue += hpg.GetComponent<HealthUnit>().healthValue;
                HPBar.Add(hpg);
                lpd.health.Add(HPGained);
            }
        }
        if(HPGained == baseHPType)
        {
            numBaseHP++;
        }
        PlaceHealthOnScreen();
    }

    public void InstantiateHealthBar()
    {
        if (lpd.health.Count == 0)
        {
            foreach (HealthUnitTypes HUT in HPTYPES)
            {
                if (HUT.HPType.GetComponent<HealthUnit>().type == baseHPType)
                {
                    for (int i = 0; i < startingHealth; i++)
                    {
                        GameObject baseHP = (GameObject)Instantiate(HUT.HPType, transform);
                        totalHealthValue += baseHP.GetComponent<HealthUnit>().healthValue;
                        HPBar.Add(baseHP);
                        lpd.health.Add(baseHPType);
                        numBaseHP++;
                    }

                    if ((startingHealth < maxHealth) && (HUT.HPType.GetComponent<HealthUnit>().takeDamageType != ""))
                    {
                        int addEmpty = maxHealth - startingHealth;
                        for (int i = 0; i < addEmpty; i++)
                        {
                            string hpPath = "Prefabs/Health/EmptyHeart";
                            GameObject hpg = (GameObject)Instantiate(Resources.Load(hpPath, typeof(GameObject)), transform) as GameObject;
                            HPBar.Add(hpg);
                            lpd.health.Add("EmptyHeart");
                        }
                    }
                }
            }
        }
        else
        {
            foreach(string HPInstance in lpd.health)
            {
                string hpPath = "Prefabs/Health/" + HPInstance;
                GameObject spawnedHP = (GameObject)Instantiate(Resources.Load(hpPath, typeof(GameObject)), transform) as GameObject;
                totalHealthValue += spawnedHP.GetComponent<HealthUnit>().healthValue;
                HPBar.Add(spawnedHP);
            }
        }
        PlaceHealthOnScreen();
    }

    public void PlaceHealthOnScreen()
    {
        for (int index = 0; index < HPBar.Count(); index++)
        {
            HPBar[index].transform.localPosition = new Vector3((float)index/2, 0f, 0f);
            HPBar[index].GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        }

    }

    public void PlayerDie()
    {
        player.gameObject.SetActive(false);
        lpd.health.Clear();
        deathOverlay.SetActive(true);
    }
}
