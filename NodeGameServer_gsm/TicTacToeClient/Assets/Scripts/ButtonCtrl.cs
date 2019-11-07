using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    public Button button;
    public Text buttonText; // O 또는 X
    public string playerMarker;  // O 또는 X

    // 버튼이 클릭되었을 때, 마커를 표시하고 버튼을 비활성화 시킨다.
    public void OnClick()
    {
        buttonText.text = GameSceneManager.instance.PlayerMark;
        button.interactable = false;  // 버튼 비활성화
    }

}
