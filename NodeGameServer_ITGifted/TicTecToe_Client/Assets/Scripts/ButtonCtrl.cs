using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 버튼을 클릭했을 때, 마커(O 또는 X)를 표시한다. */
public class ButtonCtrl : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public string playerMark;  // O 또는 X 

    // 버튼이 클릭되면 마커를 표시하고, 버튼을 비활성화 시킴.
    public void OnClick()
    {
        buttonText.text = GameController.instance.GetPlayerMark();
        button.interactable = false;  // 버튼 비활성화
        GameController.instance.EndTurn();  // 턴 종료 알림.
    }

}
