using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject chatPanel, textPrefab;
    public InputField chatBox;
    private ChatNetwork chatNetwork;

    void Start()
    {
        chatNetwork = GetComponent<ChatNetwork>();
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
    }

    // 채팅창에 Text를 추가하는 메서드
    public void SendMsgToChat(string inputText)
    {
        // public static Object Instantiate (Object original, Transform parent);
        GameObject newText = Instantiate(textPrefab, chatPanel.transform);  
        newText.GetComponent<Text>().text = inputText;
    }
}
