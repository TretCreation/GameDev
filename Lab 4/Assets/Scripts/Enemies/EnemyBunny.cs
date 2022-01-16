using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBunny : MonoBehaviour
{
    [SerializeField] float agroRange;

    [SerializeField] float cliffRange;

    [SerializeField] float moveSpeed;

    public float wanderTime;
    
    GameObject[] targetCliff;
    GameObject[] targetChicken;
    GameObject[] targetBunny;
    GameObject[] targetWolf;
    

    void Start() {
        targetCliff = GameObject.FindGameObjectsWithTag("Cliff");
        targetChicken = GameObject.FindGameObjectsWithTag("Chicken");
        targetWolf = GameObject.FindGameObjectsWithTag("Wolf");
        targetBunny = GameObject.FindGameObjectsWithTag("Bunny");
    }

    private void Update() {
        Wandering();
        iterateArray(targetCliff, cliffRange, false);
        iterateArray(targetChicken, agroRange, false);
        iterateArray(targetBunny, agroRange, false);
        iterateArray(targetWolf, agroRange, false);
    }   

    void iterateArray(GameObject[] arr, float radius, bool direction) {
        foreach(GameObject child in arr) {
            if(child != null && (Vector2.Distance(transform.position, child.transform.position) < radius)) {
                transform.position = Vector2.MoveTowards(transform.position, child.transform.position, (direction ? 1 : -1) * moveSpeed * 3f * Time.deltaTime);
            }
        }  
    }
    
    void Wandering() {
        if (wanderTime > 0) {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * 10f);
            wanderTime -= Time.deltaTime;
        } else {
            wanderTime = Random.Range(1f, 5f);
            Wander();
        }
    }

    void Wander() {
        transform.eulerAngles = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }
}

