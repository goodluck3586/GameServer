using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string UserNickname { get; set; }

    // 싱글톤 패턴
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance.gameObject);

        DontDestroyOnLoad(this.gameObject);  // 씬이 전환되도 파괴되지 않도록 한다.

        // Socket 통신 컴포넌트 추가
        this.gameObject.AddComponent<SocketIOComponent>();
        this.gameObject.AddComponent<NetworkManager>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // 0
        SceneManager.LoadScene(currentSceneIndex + 1);  // 다음 씬으로 전환됨.
    }
}
