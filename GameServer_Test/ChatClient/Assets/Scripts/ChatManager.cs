using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public InputField chatBox;
    public GameObject chatPanel, textPrefab;
    private ChatNetwork chatNetwork;

    void Start()
    {
        chatNetwork = GetComponent<ChatNetwork>();
        Invoke("JoinRequest", 1f);
    }

    private void JoinRequest()
    {
        chatNetwork.JoinMsg();
    }

    void Update()
    {
        // 키보드 입력을 감지하여, 입력된 내용을 SendMsgToChat() 메서드로 전송 
        if(chatBox.text != "")   // inputField에 text가 있으면
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //SendMsgToChat(chatBox.text);
                chatNetwork.SendNewMsg(chatBox.text);
                chatBox.text = "";
                chatBox.ActivateInputField();  // chatBox에 포커스
            }
        }
        else
        {
            if(!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
            chatNetwork.JoinMsg();
    }

    // 채팅창에 Text를 추가하는 메서드
    public void SendMsgToChat(string inputText, string userName)
    {
        // public static Object Instantiate (Object original, Transform parent);
        GameObject newText = Instantiate(textPrefab, chatPanel.transform);  

        if(userName != null)
        {
            if(inputText == "join")
                newText.GetComponent<Text>().text = "* " + userName + "님 입장 *";
            else if(inputText == "exit")
                newText.GetComponent<Text>().text = "* " + userName + "님 퇴장 *";
            else
                newText.GetComponent<Text>().text = userName + " : " + inputText;
        }
        else
        {
            print("userName이 null값 입니다.");
        }
    }
}
