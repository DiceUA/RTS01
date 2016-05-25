using UnityEngine;
using System.Collections;


public class Raycamera : MonoBehaviour 
{

    RaycastHit hit;
    float rayLength = 10f;

    // Detect where player clicked
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow);

        if(Input.GetMouseButtonDown(0))
            if (Physics.Raycast(ray, out hit) == true)
                Debug.Log("Ray hit: " + hit.transform.gameObject.name);
    }    
}