using UnityEngine;
using System.Collections;

public class BarrelBehaviour : MonoBehaviour 
{
    public GameObject round;
    public Transform barrelFire;
    public float bulletSpeed = 1000f;
    private float reloadSpeed = 1f;
    private float nextFire;
    private bool readyToShoot = true;

	// Use this for initialization
	void Awake () 
	{
        barrelFire = transform.FindChild("BarrelFire");       
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKey(KeyCode.W))
            Shoot();
	}

    /// <summary>
    /// Shoot projectile
    /// </summary>
    void Shoot()
    {
        if (Time.time > nextFire && readyToShoot)
        {
            nextFire = Time.time + reloadSpeed;
            GameObject shootBullet = Instantiate(round, barrelFire.transform.position, barrelFire.transform.rotation) as GameObject;
            shootBullet.GetComponent<Rigidbody>().AddForce(-barrelFire.transform.up * bulletSpeed);
        }
    }
}