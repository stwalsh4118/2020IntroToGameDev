using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance = null;
    public PlayerState GlobalPlayerData = new PlayerState();
    public Player player;
    public bool hasPlayerLoaded = false;

    private bool _inMenu = false;
    private bool _inCombat = false;
    private string _currentZone = "Limbo";

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
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        LoadGameState();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void LoadGameState()
    {
        //load playerstate
        player = GameObject.FindObjectOfType<Player>();
        player.LoadPlayerData();
        player.GetComponentInParent<PlayerFire>().UpdateWeaponShotPattern();


    }

}

[Serializable]
public class PlayerState
{
    public float health;
    public Weapons weapon;
    public Ability ability;
}


