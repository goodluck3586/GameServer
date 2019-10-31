using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatClient : MonoBehaviour
{
    private SocketIOComponent socket;  // SocketIOComponent 객체
    private ChatManager chatManager;  // 채팅 스크립트

    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        chatManager = GetComponent<ChatManager>();

        socket.On("open", TestOpen);  // 서버와 연결되면 발생하는 이벤트
        socket.On("error", TestError);
        socket.On("close", TestClose);  // 서버와 접속이 끊어지면 발생하는 이벤트

        socket.On("join", OnJoin);
        socket.On("broadMSG", OnBroadMsg);
        socket.On("exitUser", OnExitUser);
        //StartCoroutine(SendMsg());
    }

    //IEnumerator SendMsg()
    //{
    //    yield return new WaitForSeconds(2f);
    //    data.Add("newMsg", "lee dong yun");
    //    jdata = new JSONObject(data);
    //    socket.Emit("newMsg", jdata);  // 서버로 데이터 전송
    //}

    #region Command
    public void JoinMsg()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("userName", GameManager.instance.GetUserName());
        JSONObject joinData = new JSONObject(data);

        socket.Emit("join", joinData);
    }

    public void SendNewMsg(string msg)
    {
        Dictionary<string, string> data = new Dictionary<string, string>(); 
        data.Add("newMsg", msg);
        data.Add("userName", GameManager.instance.GetUserName());
        JSONObject jdata = new JSONObject(data);

        socket.Emit("newMsg", jdata);  // 서버로 데이터 전송
    }
    #endregion

    #region Listening Handler
    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    private void OnJoin(SocketIOEvent e)
    {
        chatManager.SendMessageToChat("join", e.data.GetField("userName").str);
    }

    private void OnBroadMsg(SocketIOEvent e)
    {
        //chatManager.SendMessageToChat("서버에서 데이터가 도착했다.");
        //Debug.Log("[SocketIO] newMsg received: " + e.name + " " + e.data);
        //Debug.Log(e.data.GetField("keyValue").str);
        chatManager.SendMessageToChat(e.data.GetField("newMsg").str, e.data.GetField("userName").str);

        //if (e.data == null) { return; }
        //Debug.Log(
        //    "#####################################################" +
        //    "THIS: " + e.data.GetField("keyValue").str +
        //    "#####################################################"
        //);
    }

    private void OnExitUser(SocketIOEvent e)
    {
        chatManager.SendMessageToChat("exit", e.data.GetField("userName").str);
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

    //#region JSON MSG Classes
    //// JSON 전송방법 2의 Object 생성을 위한 Inner Class
    //[Serializable]
    //public class JSONData
    //{
    //    public string message;

    //    public JSONData(string msg)
    //    {
    //        this.message = msg;
    //    }

    //    public static JSONData CreateFromJSON(string jsonString)
    //    {
    //        return JsonUtility.FromJson<JSONData>(jsonString);
    //    }
    //}
    //#endregion
}
