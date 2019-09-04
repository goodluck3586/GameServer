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
        socket.On("error", TestError);
        socket.On("close", TestClose);

        socket.On("broadcastMsg", OnBroadcastMsg);
    }

    #region 송신 이벤트 처리
    public void SendNewMsg(string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("msg", msg);
        JSONObject jObject = new JSONObject(data);
        socket.Emit("newMsg", jObject);
    }
    #endregion

    #region 수신 이벤트 처리
    private void OnBroadcastMsg(SocketIOEvent e)
    {
        chatManager.SendMsgToChat(e.data.GetField("msg").str);
    }

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void TestBoop(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

        if (e.data == null) { return; }
        Debug.Log("MSG: " + e.data.GetField("msg").str);  // {msg: "Hello Client!"}에서 값 추출
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