using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletPool : MonoBehaviour
{

    public static BulletPool bulletPoolInstance;
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject pooledBullet;
    }
    public List<ObjectPoolItem> itemsToPool;

    private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;

    public List<GameObject> pooledObjects;

    private void Awake()
    {
        if (bulletPoolInstance == null)
        {
            bulletPoolInstance = this;
        }

        else if (bulletPoolInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        pooledObjects = new List<GameObject>();
        bullets = new List<GameObject>();
        SceneManager.sceneLoaded += OnSceneLoad;
    }


    public GameObject GetBullet(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.pooledBullet.tag == tag)
            {
                GameObject obj = (GameObject)Instantiate(item.pooledBullet);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        pooledObjects.Clear();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

}
