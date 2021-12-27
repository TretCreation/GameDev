using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : MonoBehaviour {
    [SerializeField] float agroRange;

    [SerializeField] float cliffRange;

    [SerializeField] float moveSpeed;

    public float deathTime;
    public float wanderTime;
    
    GameObject[] targetCliff;
    GameObject[] targetChicken;
    GameObject[] targetBunny;
    GameObject[] targetPlayer;
    
    
    void Start() {
        targetCliff = GameObject.FindGameObjectsWithTag("Cliff");
        targetChicken = GameObject.FindGameObjectsWithTag("Chicken");
        targetBunny = GameObject.FindGameObjectsWithTag("Bunny");
        targetPlayer = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update() {
        iterateArray(targetCliff, cliffRange, false);
        iterateArray(targetChicken, agroRange, true);
        iterateArray(targetBunny, agroRange, true);
        iterateArray(targetPlayer, agroRange, true);
        Wandering();
        Death();
    }

    void Wandering() {
        if (wanderTime > 0) {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * 10f);
            wanderTime -= Time.deltaTime;
        } else {
            wanderTime = Random.Range(1f, 3f);
            Wander();
        }
    }
    void iterateArray(GameObject[] arr, float radius, bool direction) {
        foreach(GameObject child in arr) {
            if(child != null && (Vector2.Distance(transform.position, child.transform.position) < radius)) {
                transform.position = Vector2.MoveTowards(transform.position, child.transform.position, (direction ? 1 : -1) * moveSpeed * Time.deltaTime);
            }
        }  
    }

    void Wander() {
        transform.eulerAngles = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Cliff") && !collision.gameObject.CompareTag("Wolf")){
            if (collision.gameObject){
                Destroy(collision.gameObject);
                deathTime += 60;
            }
        }
    }

    void Death() {
        if (deathTime > 0) {
            deathTime -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
}

