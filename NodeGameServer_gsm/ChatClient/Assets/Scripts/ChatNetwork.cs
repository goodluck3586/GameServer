using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class ChatNetwork : MonoBehaviour
{
    private SocketIOComponent socket;
    private ChatManager chatManager;

    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        chatManager = GetComponent<ChatManager>();

        socket.On("open", TestOpen);
        socket.On("boop", TestBoop);
        socket.On("error", TestError);
        socket.On("close", TestClose);

        socket.On("broadcastMsg", OnBroadcastMsg);
        socket.On("broadcastJoin", OnJoin);
        socket.On("exitUser", OnExitUser);
    }

    #region 송신 이벤트 처리
    // 새로운 사용자 접속 이벤트 처리
    public void JoinMsg()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("userName", GameManager.instance.GetUsername());
        JSONObject jObject = new JSONObject(data);
        socket.Emit("join", jObject);
    }

    public void SendNewMsg(string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("msg", msg);
        JSONObject jObject = new JSONObject(data);
        socket.Emit("newMsg", jObject);
    }
    #endregion

    #region 이벤트 리스너 함수
    private void OnJoin(SocketIOEvent e)
    {
        chatManager.SendMsgToChat("join", e.data.GetField("msg").str);
    }

    private void OnBroadcastMsg(SocketIOEvent e)
    {
        chatManager.SendMsgToChat(e.data.GetField("msg").str, e.data.GetField("userName").str);
    }

    private void OnExitUser(SocketIOEvent e)
    {
        chatManager.SendMsgToChat("exitUser", e.data.GetField("msg").str);
    }


    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void TestBoop(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);  // {msg: "Hello Client!!!"}

        if (e.data == null) { return; }
        Debug.Log("데이터 추출 : " + e.data.GetField("msg").str);
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
