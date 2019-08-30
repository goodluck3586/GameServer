using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bullet에 적용
 * Bullet가 충돌하면 충돌한 Object의 Health 스크립트 컴포넌트에 접근하여 TakeDamagae() 함수 실행
 */

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerFrom;

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>(); // 충돌한 객체의 health script 컴포넌트 연결
        if (health != null)
        {
            health.TakeDamage(playerFrom, 10);
        }

        Destroy(gameObject);  // 총알 제거
    }
}
