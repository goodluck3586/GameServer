using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class NetworkManager : MonoBehaviour
{
    private SocketIOComponent socket;

    public void Start()
    {
        socket = GetComponent<SocketIOComponent>();

        socket.On("open", TestOpen);
        socket.On("boop", TestBoop);
        socket.On("error", TestError);
        socket.On("close", TestClose);

        socket.On("waitForAnotherUser", OnwaitForAnotherUser);
        socket.On("playGame", OnPlayGame);
        socket.On("requestMark", OnRequestMark);

    }

    #region 송신 이벤트
    public void Join()
    {
        print("NetworkManager.Join()");
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("userName", GameManager.instance.userName);
        JSONObject jObject = new JSONObject(data);
        socket.Emit("join", jObject);
    }

    public void RequestMark()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("userName", GameManager.instance.userName);
        JSONObject jObject = new JSONObject(data);
        socket.Emit("requestMark", jObject);
    }
    #endregion

    #region 수신 이벤트 처리
    private void OnwaitForAnotherUser(SocketIOEvent obj)
    {
        print("NetworkManager.OnwaitForAnotherUser()");
        GameObject.Find("StartManager").GetComponent<StartManager>().WriteStatusText("새로운 사용자 접속 대기중...");
    }

    private void OnPlayGame(SocketIOEvent obj)
    {
        GameManager.instance.LoadNextScene();
    }

    private void OnRequestMark(SocketIOEvent obj)
    {
        print("Your mark is " + obj.data.GetField("mark").str);
        GameController.instance.OnRequestMark(obj.data.GetField("mark").str);
    }
    #endregion

































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
}
