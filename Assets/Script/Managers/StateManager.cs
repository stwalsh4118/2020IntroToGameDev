using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance = null;
    public PlayerState GlobalPlayerData = new PlayerState();
    public Player player;
    public bool hasPlayerLoaded = false;
    public Dictionary<int, bool> EnemySpawnerState;

    private bool _inMenu = false;
    private bool _inCombat = false;
    private string _currentZone = "Limbo";
    [SerializeField]private string _currentScene = "Limbo";  

    public bool inMenu
    {
        get { return _inMenu; }
        set { _inMenu = value; }
    }

    public bool inCombat
    {
        get { return _inCombat; }
        set { _inCombat = value; }
    }

    public string currentZone
    {
        get { return _currentZone; }
        set { _currentZone = value; }
    }

    public string CurrentScene { get => _currentScene; set => _currentScene = value; }







    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of StateManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set StateManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
        DOTween.SetTweensCapacity(2000, 250);
        SceneManager.sceneLoaded += OnSceneLoad;
        player = GameObject.FindObjectOfType<Player>();
        EnemySpawnerState = new Dictionary<int, bool>();
    }

void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _currentScene = scene.name;
        LoadGameState(scene);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void LoadGameState(Scene scene)
    {
        Debug.Log("loading " + scene.name);
        //load playerstate
        player = GameObject.FindObjectOfType<Player>();
        player.LoadPlayerData();
        player.GetComponentInParent<PlayerFire>().UpdateWeaponShotPattern();
        if(scene.name == "Limbo")
        {
            GetEnemySpawnerStates();
            SetEnemySpawnerStates();
        }
    }

    void GetEnemySpawnerStates()
    {
        if(EnemySpawnerState.Count == 0)
        {
            foreach (EnemySpawner es in FindObjectsOfType<EnemySpawner>())
            {
                EnemySpawnerState.Add(es.NumSpawner,es.HasSpawned);
            }
        }
    }

    void SetEnemySpawnerStates()
    {
        foreach(EnemySpawner e in FindObjectsOfType<EnemySpawner>())
        {
            int num = e.NumSpawner;
            bool hS = EnemySpawnerState[num];

            e.HasSpawned = hS;
        }
    }

}

[Serializable]
public class PlayerState
{
    public List<string> health;
    public Weapons weapon;
    public Ability ability;
    public int numGold;
}


