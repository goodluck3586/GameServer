using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string userName;
    private string url;
    public static GameManager instance = null;

    #region Singleton Pattern
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public void SetUserName(string text)
    {
        userName = text;
        print("userName setted: " + userName);
    }

    public string GetUserName()
    {
        return userName;
    }

    public void SetUrl(string ip)
    {
        url = $"ws://{ip}/socket.io/?EIO=4&transport=websocket";
    }
    
    public string GetUrl()
    {
        return url;
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
