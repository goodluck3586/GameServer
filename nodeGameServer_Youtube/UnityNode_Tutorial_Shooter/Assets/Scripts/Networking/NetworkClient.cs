using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class NetworkClient : MonoBehaviour
{
    public SocketIOComponent socket;

    public void Start()
    {
        socket.On("open", SetupEvents);
        socket.On("News", ReceiveMsg);
    }

    void ReceiveMsg(SocketIOEvent obj)
    {
        Debug.Log(obj);
        Debug.Log(obj.data);
    }

    void SetupEvents(SocketIOEvent obj)
    {
        Debug.Log("Connection made to the server");
    }

    // Update is called once per frame
    public void Update()
    {
    }


}
