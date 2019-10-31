using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 화면상단 사용자 턴을 알려주는 패널 클래스
[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}
#endregion

public class GameController : MonoBehaviour
{
    #region 멤버 필드
    public static GameController instance;  // GameController 객체
    private NetworkManager networkManager;

    public Text[] buttonList;  // 9개 버튼의 텍스트가 연결된 배열
    public string playerMark = null;  // O, X
    public bool myTurn = false;

    private int clickCount;  // 버튼의 클릭 횟수 저장
    public GameObject gameoverPanel;  // 게임 종료시 표시되는 패널
    public Text gameoverText;  // 게임 종료시 표시되는 텍스트
    #endregion

    #region 상단의 사용자 턴을 알려주는 패널 객체
    public Player playerPanelX;
    public Player playerPanelO;

    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    #endregion

    // 싱글톤 패턴
    private void Awake()
    { 
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        networkManager = GameManager.instance.GetComponent<NetworkManager>();
        InitializBoard();
    }

    // 게임 초기화
    private void InitializBoard()
    {
        clickCount = 0;
        SetButtonInteractable(true);  // 모든 버튼 활성화/비활성화
        gameoverPanel.SetActive(false);  // 게임오버패널 비활성화
        networkManager.RequestMark();  // 서버에게 사용자 마크를 달라고 요청.
    }

    private void SetButtonInteractable(bool v)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = v;
        }
    }

    public void OnRequestMark(string mark)
    {
        playerMark = mark;
        print("playerMark 세팅됨 " + playerMark);
        myTurn = (playerMark == "X") ? true : false;
        ChangeMarksPanel("X");
    }

    private void ChangeMarksPanel(string mark)
    {
        if (mark == "X")
            SetPlayerColors(playerPanelX, playerPanelO);
        else
            SetPlayerColors(playerPanelO, playerPanelX);
    }

    private void SetPlayerColors(Player activePlayer, Player inactivePlayer)
    {
        activePlayer.panel.color = activePlayerColor.panelColor;
        activePlayer.text.color = activePlayerColor.textColor;
        inactivePlayer.panel.color = inactivePlayerColor.panelColor;
        inactivePlayer.text.color = inactivePlayerColor.textColor;
    }

    // 현재 턴의 마크(O, X) 반환
    public string GetPlayerMark()
    {
        return playerMark;
    }

    public void OnButtonClick(string btnNumber, string mark)
    {

    }

    #region 승리 조건 체크? GameOver() : ChangeMark()
    public void EndTurn()
    {
        #region 승리 조건 체크를 위한 2차원 가변 배열
        int[][] winConditionArray = new int[8][];
        winConditionArray[0] = new int[] { 0, 1, 2 };
        winConditionArray[1] = new int[] { 3, 4, 5 };
        winConditionArray[2] = new int[] { 6, 7, 8 };
        winConditionArray[3] = new int[] { 0, 3, 6 };
        winConditionArray[4] = new int[] { 1, 4, 7 };
        winConditionArray[5] = new int[] { 2, 5, 8 };
        winConditionArray[6] = new int[] { 0, 4, 8 };
        winConditionArray[7] = new int[] { 2, 4, 6 };
        #endregion

        // 승리 조건 체크
        for (int i = 0; i < winConditionArray.Length; i++)
        {
            if (CheckVictoryCondition(winConditionArray[i]))
            {
                print("누군가 승리함");
                GameOver(playerMark);  // 게임 종료 처리
                return;
            }
        }
        ChangeMark();  // player의 마크를 변경함.
    }

    private void ChangeMark()
    {
        playerMark = (playerMark == "X") ? "O" : "X";
    }

    private void GameOver(string winnerMark)
    {
        
    }

    public bool CheckVictoryCondition(int[] indexs)  // new int[] { 3, 4, 5 }
    {
        return buttonList[indexs[0]].text == playerMark &&
               buttonList[indexs[1]].text == playerMark &&
               buttonList[indexs[2]].text == playerMark;
    }
    #endregion

}
