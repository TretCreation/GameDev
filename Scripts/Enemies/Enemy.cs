using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int health;
    
    public static List<Enemy> enemyList = new List<Enemy>();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage) {
        {
            health -= damage;
            if(health <= 0){
                Destroy (gameObject);
            }
        }
    }
}
