using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinChat : MonoBehaviour
{
    [SerializeField] private InputField inputField_userName;

    public void SetUserName()
    {
        if(inputField_userName.text != null)
        {
            GameManager.instance.SetUserName(inputField_userName.text);
            GameManager.instance.LoadNextScene();
        }
        else
        {
            print("이름을 입력하세요!!!");
        }
    }
}
