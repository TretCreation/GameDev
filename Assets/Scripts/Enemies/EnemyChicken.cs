using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChicken : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float agroRange;
    [SerializeField] float cliffRange;

    private GameObject[] targetBoids;
    private GameObject[] targetCliff;
    private GameObject[] targetWolf;
    private GameObject[] targetBunny;

    private Vector3 cohesion;
    private Vector3 alignment;
    private Vector3 velocity;

    private float maxSpeed = 1f;
    

    private void Start()
    {
        InvokeRepeating("CalculateVelocity", 0, 1);
        targetBoids = GameObject.FindGameObjectsWithTag("Chicken");
        targetCliff = GameObject.FindGameObjectsWithTag("Cliff");
        targetWolf = GameObject.FindGameObjectsWithTag("Wolf");
        targetBunny = GameObject.FindGameObjectsWithTag("Bunny");
    }
    
    void CalculateVelocity()
    {
        velocity = Vector3.zero;
        cohesion = Vector3.zero;
        alignment = Vector3.zero;

        foreach (var boid in targetBoids)
        {
            if(boid != null) {
                cohesion += boid.transform.position;
                alignment += boid.GetComponent<EnemyChicken>().velocity;
            }
        }

        cohesion = cohesion / targetBoids.Length;
        cohesion = cohesion - transform.position;
        cohesion = Vector3.ClampMagnitude(cohesion, maxSpeed);

        alignment = alignment / targetBoids.Length;
        alignment = Vector3.ClampMagnitude(alignment, maxSpeed);
        
        velocity += cohesion + alignment * 1.5f;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    void Update() {
        iterateArray(targetCliff, cliffRange);
        iterateArray(targetWolf, agroRange);
        iterateArray(targetBunny, agroRange);
        
        if (transform.position.magnitude > 25){
            velocity += -transform.position.normalized;
        }
        transform.position += velocity * Time.deltaTime;

        Debug.DrawRay(transform.position, cohesion, Color.magenta);
        Debug.DrawRay(transform.position, alignment, Color.blue);
    }

    void iterateArray(GameObject[] arr, float radius) {
        foreach(GameObject child in arr) {
            if(child != null && (Vector2.Distance(transform.position, child.transform.position) < radius)) {
                velocity += cohesion + -alignment;
                velocity = Vector3.ClampMagnitude(velocity, maxSpeed);   
            }
        }  
    }
}
