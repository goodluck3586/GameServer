using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private string userName;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetUserName(string inputText = "이동윤")
    {
        this.userName = inputText;
        print("userName setted: " + userName);
    }

    public string GetUserName()
    {
        return userName;
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // LobbyScene는 0번, ChatScene는 1번
        SceneManager.LoadScene(currentSceneIndex + 1); // 다음 씬을 로드하라는 명령
    }
}
