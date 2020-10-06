using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveCameraTowardsMouse : MonoBehaviour
{
    public Vector3 CameraPosition;
    public Vector3 MousePosition;
    public Vector3 mousePos;
    public Vector3 moveCamera;
    public Transform Player;
    public Transform CameraT;
    public float XCutoff;
    public float YCutoff;
    public float sens;
    public static moveCameraTowardsMouse Instance;

    void Awake()
    {
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
        Player = GameObject.Find("Character").GetComponent<Transform>();
        CameraT = GameObject.Find("CM vcam1").GetComponent<Transform>();
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("mouseloaded");
        Player = GameObject.Find("Character").GetComponent<Transform>();
        CameraT = GameObject.Find("CM vcam1").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        MousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos.x = ((mousePos.x - .5f) * 2) * XCutoff;
        mousePos.y = ((mousePos.y - .5f) * 2) * YCutoff;
        Vector3 playerPos = Player.position;
        mousePos.z = -200;
        CameraPosition.x = playerPos.x + mousePos.x * sens;
        CameraPosition.y = playerPos.y + mousePos.y * sens;
        //CameraPosition = mousePos;
        CameraPosition.z = -2;
        CameraT.position = CameraPosition;
        //GetComponent<Transform>().position = Player.position;
        //GetComponent<Transform>().position = MousePosition;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
