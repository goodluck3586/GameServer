using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string userName = "사용자 닉네임";
    public string url = "127.0.0.1:4567";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        // 네트워크 통신에 필요한 스크립트를 컴포넌트로 추가함.
        this.gameObject.AddComponent<SocketIOComponent>();
        this.gameObject.AddComponent<NetworkManager>();

        DontDestroyOnLoad(this.gameObject);
    }

    // 새로운 사용자 접속 요청
    public void Join()
    {
        print("GameManager.Join()");
        GetComponent<NetworkManager>().Join();  // 서버에게 조인 요청 보냄.
    }

    // 다음 씬으로 이동
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // 씬의 빌드번호
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


}
