using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public int maxMessages = 50;  // 보여지는 최대 메시지 개수
    public GameObject chatPanel, textPrefab;  // Content 패널과 Text 객체
    public InputField chatBox;
    private ChatClient chatClient;

    [SerializeField]  List<Message> messageList = new List<Message>();

    private void Start()
    {
        chatClient = GetComponent<ChatClient>();
        Invoke("JoinRequest", 1f);
    }

    private void JoinRequest()
    {
        chatClient.JoinMsg();
    }

    private void Update()
    {
        if(chatBox.text != "")  // inputField에 text가 있으면
        {
            if (Input.GetKeyDown(KeyCode.Return))  // Enter Key가 눌리면
            {
                //SendMessageToChat(chatBox.text);
                chatClient.SendNewMsg(chatBox.text);
                chatBox.text = "";
                chatBox.ActivateInputField();
            }
        }
        else
        {
            if(!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

        if (!chatBox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessageToChat("You pressed the space key!", "스페이스바 눌림");
            }
        }
        
    }

    public void SendMessageToChat(string inputText, string userName)
    {
        // messageList의 message가 50개를 넘으면 최초 message 삭제
        if (messageList.Count >= maxMessages)
        {
            messageList.Remove(messageList[0]);
            Destroy(messageList[0].textObj.gameObject);
        }

        // Message 저장 객체에 inputText 저장
        Message newMessage = new Message();
        newMessage.text = inputText;

        // Text UI 객체를 생성하고, Message 저장 객체의 textObj로 할당
        GameObject newText = Instantiate(textPrefab, chatPanel.transform);  // public static Object Instantiate (Object original, Transform parent);
        newMessage.textObj = newText.GetComponent<Text>();

        if(userName != null)
        {
            if (GameManager.instance.GetUserName() == userName)
                newMessage.textObj.color = new Color32(255, 255, 255, 255);
            else
                newMessage.textObj.color = new Color32(255, 150, 150, 255);

            if (inputText == "join")
                newMessage.textObj.text = "**********  " + userName + "  님이 입장 하셨습니다. **********";
            else if(inputText == "exit")
                newMessage.textObj.text = "**********  " + userName + "  님이 퇴장 하셨습니다. **********";
            else
                newMessage.textObj.text = userName + " : " + inputText;
        }
        else
        {
            print("userName이 null값 입니다.");
        }
        
        messageList.Add(newMessage);
    }
}

// 입력된 string과 Text Object로 구성된 클래스
[System.Serializable]
public class Message
{
    public string text;
    public Text textObj;
}
