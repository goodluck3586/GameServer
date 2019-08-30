using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player와 Monster의 HealthBar Canvas에 적용
 * HealthBar가 항상 메인 카메라를 정면에서 바라보도록 위치 수정
 */
public class Billboard : MonoBehaviour
{
    void Update()
    {
        // Healthbar Canvas가 항상 카메라에 정면으로 보이도록 위치를 수정
        transform.LookAt(Camera.main.transform);
    }
}
