using UnityEngine;
using System.Collections;

public class UnitTank : MonoBehaviour
{

    #region Variables

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
    public Transform barrel;
    public Transform turret;
    public Transform barrelFire; //Empty GameObject on barrel end to know where barrel facing
    public Transform body;
    public string enemyString;

    //Здесь будут глобальные переменные для тестовых функций
    private float newY = 0; 

    #endregion

    void Awake()
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

    void Update()
    {
        UnitMovement(newPosition);
        GetTarget();
        Shoot();
    }

    // Оставлю пока на память, потом придумаю что с этим делать.
    //void UnitMovement()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Physics.Raycast(ray, out hit);
    //    newPosition = hit.point;
    //    float offset = 0.5f;
    //    float speed = 3f;
    //    float distance = Vector3.Distance(body.transform.position, newPosition);
    //    Vector3 targetDir = (newPosition - body.transform.position).normalized;
    //    float step = speed * Time.deltaTime;
    //    if (targetDir != Vector3.zero)
    //    {
    //        Quaternion newRotation = Quaternion.LookRotation(targetDir);
    //        newRotation.x = 0;
    //        newRotation.z = 0;
    //        body.rotation = Quaternion.Slerp(body.transform.rotation, newRotation, step);
    //        if (distance > offset)
    //            transform.position += body.transform.forward * Time.deltaTime * speed;
    //    }
    //}

    // Передвижение юнита. Получает точку на карте куда нужно двигаться. 
    /// <summary>
    /// Move unit to click point
    /// </summary>
    /// <param name="point">Vector3</param>
    void UnitMovement(Vector3 point)
    {
        newPosition = point;
        float offset = 1.5f;
        float speed = 3f;
        float distance = Vector3.Distance(body.transform.position, newPosition);
        Vector3 targetDir = (newPosition - body.transform.position).normalized;
        float step = speed * Time.deltaTime;
        if (targetDir != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(targetDir);
            newRotation.x = 0;
            newRotation.z = 0;
            body.rotation = Quaternion.Slerp(body.transform.rotation, newRotation, step);
            if (distance > offset)
                transform.position += body.transform.forward * Time.deltaTime * speed;
        }
    }

    // Стрелять по цели. В данный момент стреляем только после прицеливания.
    /// <summary>
    /// Shoot target if target locked
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

    // Поиск цели. Вращает башню к ближайшему вроженскому объекту. Пока так, потом будем усложнять.
    /// <summary>
    /// Get target, aim turret on it
    /// </summary>
    void GetTarget()
    {
        float speed = 5f;
        float step = speed * Time.deltaTime;
        if (GameObject.Find(enemyString) != null)
        {
            target = GameObject.Find(enemyString).transform;            
            Vector3 targetDir = (target.position - turret.transform.position).normalized;            
            if (targetDir != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(targetDir);
                newRotation.x = 0;
                newRotation.z = 0;
                turret.rotation = Quaternion.Lerp(turret.transform.rotation, newRotation, step);
                Debug.DrawRay(barrelFire.position, barrelFire.forward * 10f, Color.red);
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.LookRotation(body.forward);
            newRotation.x = 0;
            newRotation.z = 0;
            turret.rotation = Quaternion.Lerp(turret.transform.rotation, newRotation, step);
        }
    }



    #region Test functions

    void RotateTurret(float y)   
    {
        newY += y;
        turret.rotation = Quaternion.Euler(currentRotation.x, newY * 2, currentRotation.z);
    }

    void ManualBodyRotation(float y)
    {
        newY += y;
        body.transform.rotation = Quaternion.Euler(currentBodyRotation.x, newY * 3, currentBodyRotation.z);
    }

    void ManualBodyMovement()
    {
        transform.position += body.transform.forward * Time.deltaTime;
    }

    #endregion
}