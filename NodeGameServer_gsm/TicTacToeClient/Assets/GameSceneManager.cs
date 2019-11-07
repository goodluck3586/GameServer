using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player  // 화면 상단에 배치된, 어떤 사용자의 턴인지를 알려주는 패널과 텍스트 객체
{
    public Image panel;
    public Text text;
}

[System.Serializable]
public class PlayerColor  // 화면 상단에 배치된, 어떤 사용자의 턴인지를 알려주는 패널과 텍스트 객체의 컬러
{
    public Color panelColor;
    public Color textColor;
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance = null;
    private NetworkManager networkManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public string PlayerMark { get; set; } = null; // O or X => 서버가 정해준다.
    public bool IsMyTurn { get; set; } = false;
    private int clickCount;  // 9번 클릭되면 Draw처리

    // UI 요소 연결
    public Text[] buttonList;  // 9개 버튼의 Text 오브젝트 연결
    public GameObject gameoverPanel;
    public Text gameoverText;
    public GameObject restartButton;
    public Player playerPanelX;
    public Player playerPanelO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    void Start()
    {
        networkManager = GameManager.instance.GetComponent<NetworkManager>();
        InitializeBoard();
    }

    // 게임 보드 초기화
    private void InitializeBoard()
    {
        clickCount = 0;
        networkManager.RequestMark();  // 서버에게 자신의 Mark가 무엇인지 요청
    }

    public void WhoseTurn(string mark)
    {
        //IsMyTurn = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
