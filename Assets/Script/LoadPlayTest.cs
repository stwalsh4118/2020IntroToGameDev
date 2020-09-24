using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadPlayTest : MonoBehaviour
{
  public void loadPlayTest()
    {
        GameObject.Find("Boss").GetComponent<BossMovement>().Arena = GameObject.Find("PlayTest");
        GameObject.Find("Boss").GetComponent<CommandReader>().loadCommands();
        GameObject.Find("Boss").GetComponent<BossMovement>().UpdateArenaCenter();
        GameObject.Find("CM vcam1").transform.position = new Vector3(0f, 40f, -2f);
    }

    public void loadEditor()
    {
        GameObject.Find("Boss").GetComponent<BossMovement>().Arena = GameObject.Find("ArenaBounds");
        GameObject.Find("Boss").GetComponent<BossMovement>().UpdateArenaCenter();
        GameObject.Find("CM vcam1").transform.position = new Vector3(0f, 4f, -2f);
    }

    public void loadPlayTestWC()
    {
        SceneManager.LoadScene("PlayTest");
    }
}
