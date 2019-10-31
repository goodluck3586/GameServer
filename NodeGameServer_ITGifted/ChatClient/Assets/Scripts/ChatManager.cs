using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public InputField chatBox;
    public GameObject chatPanel, textPrefab;
    private ChatNetwork chatNetwork;

    private void Start()
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
        if(chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // 새로운 textPrefab을 Content에 추가
                //SendMsgToChat(chatBox.text);  // 로컬로 화면에 출력
                chatNetwork.SendNewMsg(chatBox.text);
                chatBox.text = "";
                chatBox.ActivateInputField();   // 채팅창으로 포커스가 이동한다.
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

    }

    public void SendMsgToChat(string inputText, string userName)
    {
        GameObject newText = Instantiate(textPrefab, chatPanel.transform);

        // 나와 상대방의 채팅 텍스트 컬러를 다르게 세팅
        if(userName == GameManager.instance.GetUserName())
        {
            newText.GetComponent<Text>().color = Color.white;
        }
        else
        {
            newText.GetComponent<Text>().color = Color.red;
        }

        if(inputText == "join")
        {
            // 새로운 사용자 입장
            newText.GetComponent<Text>().text = userName + " 님이 입장하셨습니다.";
        }
        else
        {
            // 채팅 메시지
            newText.GetComponent<Text>().text = userName + " : " + inputText;
        }
    }
}
