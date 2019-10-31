using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public InputField inputField;  // 사용자 닉네임을 입력받음.
    public Text gameStatusText;    // 게임 상태 표시

    /* inputField에 입력된 닉네임을 GameManager에 저장 */
    public void SetUserName()
    {
        GameManager.instance.userName = inputField.text;
    }

    /* 서버로부터 내려온 상태 메시지를 화면에 표시 */
    public void WriteStatusText(string msg)
    {
        print("StartManager.WriteStatusText()");
        gameStatusText.text = msg;
    }
}
