using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private SocketIOComponent socket;

    public void Start()
    {
        socket = GameManager.instance.GetComponent<SocketIOComponent>();

        socket.On("open", TestOpen);
        socket.On("boop", TestBoop);
        socket.On("error", TestError);
        socket.On("close", TestClose);
        socket.On("waitAnotherUser", OnWaitAnotherUser);
        socket.On("playGame", OnPlayGame);
    }

    #region 송신 이벤트
    public void Join()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("userNickname", GameManager.instance.UserNickname);
        JSONObject jSONObject = new JSONObject(data);
        socket.Emit("join", jSONObject);
    }
    #endregion

    #region 수신 이벤트 처리 메소드
    private void OnWaitAnotherUser(SocketIOEvent obj)
    {
        GameObject.Find("StartSceneManager").GetComponent<StartSceneManager>().WriteStatusText("대기중...");
    }

    private void OnPlayGame(SocketIOEvent obj)
    {
        GameManager.instance.LoadNextScene();  // 다음 씬으로 전환
    }

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void TestBoop(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }

    public void TestError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }
    #endregion
}