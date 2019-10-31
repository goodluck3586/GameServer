using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinChat : MonoBehaviour
{
    [SerializeField] private InputField inputField_userName;

    // Join 버튼이 클릭되면 처리되는 메소드
    public void SetUserName()
    {
        if(inputField_userName.text != "")
        {
            GameManager.instance.SetUserName(inputField_userName.text);  // 닉네임 저장
            GameManager.instance.LoadNextScene();  // ChatScene으로 전환
        }
        else
        {
            print("닉네임이 입력되지 않았습니다.");
        }
        
    }
}
