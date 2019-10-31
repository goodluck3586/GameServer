using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private string userName;

    // 싱글톤 패턴(유일하게 하나의 객체만 유지)
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);  // 씬이 전환되도 파괴되지 않도록 함.
    }

    public void SetUserName(string name)
    {
        this.userName = name;
        print("user name setted: " + userName);
    }

    public string GetUsername()
    {
        return userName;
    }

    public void LoadNextScene()
    {
        int currentSceneNum = SceneManager.GetActiveScene().buildIndex;  // 0
        SceneManager.LoadScene(currentSceneNum + 1);
    }
}
