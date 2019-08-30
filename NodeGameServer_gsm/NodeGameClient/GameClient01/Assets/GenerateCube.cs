using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCube : MonoBehaviour
{
    public GameObject cube;
    Vector3 respawnPosition;

    void Start()
    {
        for(int i=0; i<100; i++)
        {
            respawnPosition = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(5.0f, 8.0f), Random.Range(-5.0f, 5.0f));
            Instantiate(cube, respawnPosition, Quaternion.identity);
        }
        
    }
}
