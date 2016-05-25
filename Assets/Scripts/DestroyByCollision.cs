using UnityEngine;
using System.Collections;

public class DestroyByCollision : MonoBehaviour 
{
    public void OnTriggerEnter(Collider other)
    {
        // Destroy enemy when it hit
        // destroy projectile
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
}