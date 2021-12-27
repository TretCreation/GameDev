using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") ^ collision.CompareTag("Wolf") ^ collision.CompareTag("Chicken") ^ collision.CompareTag("Bunny"))
        {
            Destroy(collision.gameObject);
        }
    }
}

