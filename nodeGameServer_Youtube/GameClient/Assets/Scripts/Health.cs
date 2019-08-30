using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player, Enemy 오브젝트에 적용
 * 
 */
public class Health : MonoBehaviour
{
    public const int maxHealth = 100;
    public bool destroyOnDeath;
    public int currentHealth = maxHealth;  // health 초기화
    public bool isEnemy = false;

    public RectTransform healthBar;  // Playr, Enemy 오브젝트의 HealthBar Canvas에 포함된 Foreground 이미지
    private bool isLocalPlayer;

    void Start()
    {
        PlayerController pc = GetComponent<PlayerController>();  // PlayerController 스크립트 연결
        isLocalPlayer = pc.isLocalPlayer;  // local player인지 network player인지 체크
    }

    // gameobject의 생명력을 감소시키는 메소드
    public void TakeDamage(GameObject playerFrom, int amount)
    {
        currentHealth -= amount;
        //TODO networking
        OnChangeHealth();
    }

    // 변경된 생명력을 화면에 표시하는 메소드
    public void OnChangeHealth()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)  // EnemySpawner객체의 SpawnEnemies()메서드에서 true로 만듬. 
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;
                healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;
            Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);
            transform.position = spawnPoint;
            transform.rotation = spawnRotation;
        }
    }
}
