using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* I'm Ready 버튼 클릭시 UserNickname을 GameManager에 저장하고, NetworkManager의 Join() 메소드 호출 */
public class StartSceneManager : MonoBehaviour
{
    public InputField inputField;
    [SerializeField] Text statusText;

    // UserNickname을 GameManager에 저장하는 메소드
    public void SetUserNickname()
    {
        GameManager.instance.UserNickname = inputField.text;
    }

    // NetworkManager의 Join() 호출 메소드: 새로운 사용자 접속을 서버에게 알림
    public void Join()
    {
        GameManager.instance.GetComponent<NetworkManager>().Join();
    }
    
    public void WriteStatusText(string msg)
    {
        statusText.text = msg;
    }

}
