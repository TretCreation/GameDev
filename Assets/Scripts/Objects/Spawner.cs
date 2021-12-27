using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform wolfPrefab;
    public Transform chiickenPrefab;
    public Transform bunnyPrefab;
    public int wolfCount;
    public int bunnyCount;


    void Start()
	{
        int chiickenCount = Random.Range(3, 11);
        for (int i = 0; i < wolfCount; i++)
        {
            Instantiate(wolfPrefab, Random.insideUnitSphere * 13, Quaternion.identity);
        }
        for (int i = 0; i < chiickenCount; i++){
            Instantiate(chiickenPrefab, Random.insideUnitSphere * 13, Quaternion.identity);
        }
        for (int i = 0; i < bunnyCount; i++){
            Instantiate(bunnyPrefab, Random.insideUnitSphere * 13, Quaternion.identity);
        }
    }
}
