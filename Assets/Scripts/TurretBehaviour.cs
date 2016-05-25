using UnityEngine;
using System.Collections;

/// <summary>
/// This class created to debug turret rotation
/// </summary>
public class TurretBehaviour : MonoBehaviour 
{
    private Vector3 currentRotation;
    private float newY = 0;

    void Awake ()
    {
        currentRotation = transform.rotation.eulerAngles;
    }
    
	void Update () 
	{
        if (Input.GetKey(KeyCode.D))
            RotateTurret(1);
        if (Input.GetKey(KeyCode.A))
            RotateTurret(-1);	
	}
    /// <summary>
    /// Rotate turret
    /// </summary>
    /// <param name="y"></param>
    void RotateTurret(float y)
    {
        newY += y;
        transform.rotation = Quaternion.Euler(currentRotation.x, newY * 2, currentRotation.z);
    }
}