using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatJoin : MonoBehaviour
{
    [SerializeField] InputField inputField_ip;
    [SerializeField] InputField inputField_nickname;

    public void JoinChatting()
    {
        SetUrl();

        if (inputField_nickname.text != "")
        {
            GameManager.instance.SetUserName(inputField_nickname.text);
            GameManager.instance.LoadNextScene();
        }
        else
        {
            print("입력된 내용이 없습니다.");
        }
    }

    private void SetUrl()
    {
        if (inputField_ip.text != "")
        {
            GameManager.instance.SetUrl(inputField_ip.text);
        }
        else
        {
            GameManager.instance.SetUrl("127.0.0.1:4567");
        }
    }
}
