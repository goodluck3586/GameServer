using System.Collections;
using UnityEngine;
using SocketIO;
using System;
using System.Collections.Generic;

/* 클라이언트 코드 */
public class TestSocketIO : MonoBehaviour
{
	private SocketIOComponent socket;
    
    // JSON Data를 처리하기 위한 변수
    Dictionary<string, string> data = new Dictionary<string, string>();
    JSONObject jsonData;

    public void Start() 
	{
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

        // 클라이언트의 소켓 요청 처리하는 메서드 연결 
        socket.On("open", TestOpen);  // 서버에서 자동으로 "open" 이벤트를 보냄.
		
        socket.On("GameStart", OnGameStart);
        socket.On("GamePlay", OnGamePlay);
        socket.On("BroadcastMSG", OnBroadcastMSG);
        socket.On("GameExit", OnGameExit);
        socket.On("UserOut", OnUserOut);

        socket.On("error", TestError);
        socket.On("close", TestClose);

        StartCoroutine("ServerTest");
    }

    public void TestOpen(SocketIOEvent e)
    {
        if (e.data == null) { return; }
        Debug.Log(e);
        Debug.Log("Open received: " + e.name + " " + e.data);
    }

    private void OnGameStart(SocketIOEvent e)
    {
        Debug.Log("OnGameStart()함수 실행됨.");
        Debug.Log(e);
    }

    private void OnGamePlay(SocketIOEvent e)
    {
        Debug.Log("OnGamePlay()함수 실행됨.");
        Debug.Log(e);
        data = e.data.ToDictionary();
        string temp;
        data.TryGetValue("name", out temp);
        Debug.Log("서버에서 받은 data string : " + temp);

        if (e.data.ToDictionary().ContainsKey("name")) Debug.Log("name is here!!");
        else Debug.Log("name is not here!!");
    }
    private void OnBroadcastMSG(SocketIOEvent e)
    {
        Debug.Log("BroadcastMSG()함수 실행됨.");
        Debug.Log(e);
    }

    private void OnGameExit(SocketIOEvent e)
    {
        Debug.Log("OnGameExit()함수 실행됨.");
        Debug.Log(e);
    }

    private void OnUserOut(SocketIOEvent e)
    {
        Debug.Log("OnUserOut()함수 실행됨.");
        Debug.Log(e);
    }


    private IEnumerator ServerTest()
	{
		// wait 1 seconds and continue
		yield return new WaitForSeconds(2);
		
		socket.Emit("GameStart");
		
		// wait 3 seconds and continue
		yield return new WaitForSeconds(3);

        data.Add("firstData", "첫 번째 데이터");
        data.Add("secondData", "두 번째 데이터");
        jsonData = new JSONObject(data);
        socket.Emit("GamePlay", jsonData);

		yield return null;  // wait ONE FRAME and continue

    }

    public void GameExitEmit()
    {
        socket.Emit("GameExit");
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
