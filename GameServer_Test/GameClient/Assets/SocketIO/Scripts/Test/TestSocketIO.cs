using System.Collections;
using UnityEngine;
using SocketIO;
using System.Collections.Generic;
using System;

public class TestSocketIO : MonoBehaviour
{
	private SocketIOComponent socket;  // SocketIOComponent 객체

    // JSON 데이터 전송을 위한 객체
    Dictionary<string, string> data = new Dictionary<string, string>();  // JSON 전송방법 1
    JSONObject jdata1, jdata2;  // JSONObject = SocketIO for Unity에서 제공하는 JSON 전송 객체

    public void Start() 
	{
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		socket.On("open", TestOpen);  // 서버와 연결되면 발생하는 이벤트
		socket.On("boop", TestBoop);
        socket.On("broadMSG", TestBroadMSG);
		socket.On("error", TestError);
		socket.On("close", TestClose);  // 서버와 접속이 끊어지면 발생하는 이벤트

        StartCoroutine(ConnectToServer()); 

        #region JSON 타입의 전송 객체에 데이터 넣기
        // JSON 전송방법 1
        data.Add("hi", "Node Server");
        data.Add("hello", "World");
        jdata1 = new JSONObject(data);

        // JSON 전송방법 2
        JSONData myObject = new JSONData(1, 45.5f, "lee dong yun");
        //myObject.level = 1;
        //myObject.timeElapsed = 45.5f;
        //myObject.playerName = "lee dong yun";
        string jsonData = JsonUtility.ToJson(myObject);  // object를 JSON string으로 변환
        //print($"jsonData: {jsonData}");
        jdata2 = new JSONObject(jsonData);
        #endregion
    }

    #region Commands
    private IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(2);  // wait 2 seconds and continue
        socket.Emit("beep", jdata1);

        yield return null;  // wait ONE FRAME and continue
        socket.Emit("beep", jdata1);

        yield return new WaitForSeconds(2);
        socket.Emit("beep", jdata2);

        yield return null;
        socket.Emit("beep", jdata2);
    }
    #endregion

    #region Listening Handler
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

    private void TestBroadMSG(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);
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

    #region JSON MSG Classes
    // JSON 전송방법 2의 Object 생성을 위한 Inner Class
    [Serializable]
    public class JSONData
    {
        public int level;
        public float timeElapsed;
        public string playerName;

        public JSONData(int level, float timeElapsed, string playerName)
        {
            this.level = level;
            this.timeElapsed = timeElapsed;
            this.playerName = playerName;
        }
    }
    #endregion
}


