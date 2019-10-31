using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [SerializeField] InputField chatBox;
    [SerializeField] GameObject chatPanel, textPrefab;
    private ChatNetwork chatNetwork;

    void Start()
    {
        chatNetwork = GetComponent<ChatNetwork>();
        Invoke("JoinRequest", 0.1f);
    }

    // 새로운 사용자 입장 처리
    private void JoinRequest()
    {
        chatNetwork.JoinMsg();
    }

    void Update()
    {
        // 만약 inputField에 새로운 텍스트가 있으면
        if(chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))  // Enter키가 눌렸으면
            {
                //SendMsgToChat(chatBox.text);  // 입력된 텍스트를 화면에 출력
                chatNetwork.SendNewMsg(chatBox.text);  // 서버로 새로운 텍스트 송신
                chatBox.text = "";
                chatBox.ActivateInputField();  // 포커스가 입력창으로 오도록 한다.
            }
        }
        else
        {
            // 만약 입력차에 포커스가 없다면, Enter 키가 눌렸을 때 포커스가 입력창으로 가도록 설정
            if(!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }
        
    }

    // 채팅창에 새로운 텍스트 오브젝트 추가
    public void SendMsgToChat(string inputText, string userName)
    {
        GameObject newText = Instantiate(textPrefab, chatPanel.transform);  // textPrefab 클론 생성
        if(inputText == "join")
        {
            newText.GetComponent<Text>().text = $"{userName}님이 입장하셨습니다.";
        }else if(inputText == "exitUser")
        {
            newText.GetComponent<Text>().text = $"{userName}님이 퇴장하셨습니다.";
        }
        else
        {
            newText.GetComponent<Text>().text = $"{userName}: {inputText}";  // inputField의 텍스트를 textPrefab에 작성
        }
        
    }
}
