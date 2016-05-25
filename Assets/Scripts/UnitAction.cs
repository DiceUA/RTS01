using UnityEngine;
using System.Collections;

public class UnitAction : MonoBehaviour 
{

    private LandUnit unit;
    private RaycastHit hit;
    private Vector3 newPosition;
    public GameObject bullet;
    private Ray findTarget;
    public float bulletSpeed = 1500f;
    private float reloadSpeed = 2f;
    private float nextFire;
    private Transform target;
    public bool readyToShoot = false;
    private Vector3 currentRotation;
    private Vector3 currentBodyRotation;
    private float newY = 0;
    public Transform barrel;
    public Transform turret;
    public Transform barrelFire;
    public Transform body;
    public string enemyString;

    void Awake ()
    {
        enemyString = "Enemy(Clone)";
        unit = new LandUnit("M1A1 Abrams");
        unit.Type = "Tank";
        turret = transform.FindChild("Main_Turret");
        barrel = turret.FindChild("Main_Barrel");
        barrelFire = barrel.FindChild("BarrelFire");
        body = transform.FindChild("Main_Body");
        newPosition = transform.position;
        currentRotation = turret.rotation.eulerAngles;
        currentBodyRotation = transform.rotation.eulerAngles;
    }

    void Update ()
    {               
        UnitMovement();
        GetTarget();        
        Shoot();
    }

    /// <summary>
    /// Moves unit to clicked point
    /// </summary>
    void UnitMovement ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        //Debug.Log("Ray Target:" + hit.point);

        if (Input.GetMouseButtonDown(1))
            newPosition = hit.point;

        float offset = 0.5f;
        float distance = Vector3.Distance(body.transform.position, newPosition);
        float speed = 3f;
        Vector3 targetDir = (newPosition - body.transform.position).normalized;
        float step = speed * Time.deltaTime;
        if(targetDir != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(targetDir);
            newRotation.x = 0;
            newRotation.z = 0;
            body.rotation = Quaternion.Slerp(body.transform.rotation, newRotation, step);
            if (distance > offset)
                transform.position += body.transform.forward * Time.deltaTime * speed;
        }        
    }    

    /// <summary>
    /// Shoot projectile if tagret is enemy
    /// </summary>
    void Shoot()
    {
        Ray ray = new Ray(barrelFire.position, barrelFire.forward);
        RaycastHit hitTarget;
        if (Physics.Raycast(ray, out hitTarget) == true)
        {
            if (hitTarget.transform.gameObject.name == enemyString)
                readyToShoot = true;
            else
                readyToShoot = false;
        }
        else
            readyToShoot = false;
    
        if (Time.time > nextFire && readyToShoot)
        {
            nextFire = Time.time + reloadSpeed;
            GameObject shootBullet = Instantiate(bullet, barrelFire.position, barrelFire.rotation) as GameObject;
            shootBullet.GetComponent<Rigidbody>().AddForce(barrelFire.forward * bulletSpeed);
        }                
    }

    /// <summary>
    /// Get target to shoot
    /// </summary>
    void GetTarget ()
    {
        if (GameObject.Find(enemyString) != null)
        {
            target = GameObject.Find(enemyString).transform;
            //float distance = Vector3.Distance(turret.transform.position, target.transform.position);
            float speed = 5f;
            Vector3 targetDir = (target.position - turret.transform.position).normalized;
            float step = speed * Time.deltaTime;
            //Vector3 newDir = Vector3.RotateTowards(turret.forward, targetDir, step, 0.0F);
            //newDir.y = 0;
            //Debug.DrawRay(turret.position, newDir * 10f, Color.red);
            //if (distance < 15f)
            //    turret.rotation = Quaternion.LookRotation(newDir);
            if (targetDir != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(targetDir);
                newRotation.x = 0;
                newRotation.z = 0;
                turret.rotation = Quaternion.Slerp(turret.transform.rotation, newRotation, step);
                Debug.DrawRay(barrelFire.position, barrelFire.forward * 10f, Color.red);
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.LookRotation(body.forward);
            newRotation.x = 0;
            newRotation.z = 0;
            turret.rotation = Quaternion.Slerp(turret.transform.rotation, newRotation, Time.deltaTime * 5f);
        }           
    }



    #region Debug functions

    /// <summary>
    /// Debug function, to test body rotation
    /// </summary>
    /// <param name="y"></param>
    void ManualBodyRotation(float y)
    {
        newY += y;
        body.transform.rotation = Quaternion.Euler(currentBodyRotation.x, newY * 3, currentBodyRotation.z);
    }
    /// <summary>
    /// Debug function to test body movement
    /// </summary>
    void ManualBodyMovement()
    {
        transform.position += body.transform.forward * Time.deltaTime;
    }

    /// <summary>
    /// Debug function to test turret rotation
    /// </summary>
    /// <param name="y"></param>
    void RotateTurret(float y)
    {
        newY += y;
        turret.rotation = Quaternion.Euler(currentRotation.x, newY * 2, currentRotation.z);
    }
    #endregion



}