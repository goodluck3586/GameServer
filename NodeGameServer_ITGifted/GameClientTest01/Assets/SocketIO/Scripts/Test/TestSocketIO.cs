using System.Collections;
using UnityEngine;
using SocketIO;
using System.Collections.Generic;
using System;

public class TestSocketIO : MonoBehaviour
{
	private SocketIOComponent socket;

    // JSON 데이터 전송을 위한 객체
    Dictionary<string, string> data = new Dictionary<string, string>();  // JSON 전송방법 1
    JSONObject jdata1, jdata2;
   
	public void Start() 
	{
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		socket.On("boop", TestBoop);
		socket.On("error", TestError);
		socket.On("close", TestClose);
        socket.On("broadMSG", TestBreadMSG);

        // JSON 전송방법 1
        data.Add("hi", "Node Server");
        data.Add("hello", "World");
        jdata1 = new JSONObject(data);

        // JSON 전송방법 2
        JSONData myObject = new JSONData();
        myObject.level = 1;
        myObject.timeElapsed = 45.5f;
        myObject.playerName = "lee dong yun";
        string jsonData = JsonUtility.ToJson(myObject);
        jdata2 = new JSONObject(jsonData);

        StartCoroutine(BeepBoop());
	}

    private void TestBreadMSG(SocketIOEvent e)
    {
        Debug.Log("broadMSG 수신: " + e);
    }

    private IEnumerator BeepBoop()
	{
		// wait 1 seconds and continue
		yield return new WaitForSeconds(2);

        socket.Emit("beep", jdata1);

        // wait 3 seconds and continue
        yield return null;

        socket.Emit("beep", jdata1);
		
		// wait 2 seconds and continue
		yield return new WaitForSeconds(2);
		
		socket.Emit("beep", jdata2);
		
		// wait ONE FRAME and continue
		yield return null;
		
		socket.Emit("beep", jdata2);
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

// JSON 전송방법 2의 Object 생성을 위한 Class
[Serializable]
public class JSONData
{
    public int level;
    public float timeElapsed;
    public string playerName;
}
