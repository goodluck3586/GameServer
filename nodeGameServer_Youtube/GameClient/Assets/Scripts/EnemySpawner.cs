using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawnPoint;  // Prefab(Empty GameObject)
    public int numberOfEnemies;    // 생성할 enemy 숫자
    [HideInInspector]
    public List<SpawnPoint> enemySpawnPoints;  // SpawnPoint 객체 배열

    void Start()
    {
        // 지정된 개수만큼 랜덤한 위치의 SpawnPoint를 생성하여 enemySpawnPoints에 저장한다.
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
            var spawnRotation = Quaternion.Euler(0f, Random.Range(0, 180), 0f);
            SpawnPoint enemySpawnPoint = (Instantiate(spawnPoint, spawnPosition, spawnRotation) as GameObject).GetComponent<SpawnPoint>();
            enemySpawnPoints.Add(enemySpawnPoint);
        }

        SpawnEnemies();  // enemy 오브젝트들 생성
    }

    public void SpawnEnemies(/*TODO networking*/)
    {
        //TODO networking
        int i = 0;
        foreach (SpawnPoint sp in enemySpawnPoints)
        {
            Vector3 position = sp.transform.position;
            Quaternion rotation = sp.transform.rotation;
            GameObject newEnemy = Instantiate(enemy, position, rotation) as GameObject;
            newEnemy.name = i + "";
            PlayerController pc = newEnemy.GetComponent<PlayerController>();
            Health h = newEnemy.GetComponent<Health>();
            h.currentHealth = 100;
            h.OnChangeHealth();
            h.destroyOnDeath = true;
            h.isEnemy = true;
            i++;
        }
    }
}
