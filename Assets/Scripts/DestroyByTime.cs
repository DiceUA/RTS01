using UnityEngine;
using System.Collections;
using System.Timers;

/// <summary>
/// Destroy gameobject by timer to prevent memory leaks
/// </summary>
public class DestroyByTime : MonoBehaviour 
{
    private float lifeTime = 3f;
	void Update () 
	{
        Destroy(gameObject, lifeTime);
	}
}