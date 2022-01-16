using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shotpos;
    public GameObject Bullet;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && NumberOfBullets.NumberOfBulletsText > 0)
        {
            Instantiate(Bullet, shotpos.transform.position, transform.rotation);
            NumberOfBullets.NumberOfBulletsText -= 1;
        }
    }
}
