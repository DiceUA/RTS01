using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour 
{
    private float rayLength = 10f;
    public Camera detectionCamera;
    private static Camera _camera;

    void Start ()
    {
        //Определяем камеру
        if (detectionCamera != null)
            _camera = detectionCamera;
        else
            _camera = Camera.main;
    }

    public void Update()
    {
        //Генерация луча для определения кликов на карте
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Debug информация
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow); 
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
                Debug.Log("You Left Clicked: " + hit.transform.gameObject.name);
            if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("You Right Clicked: " + hit.transform.gameObject.name);
                GiveOrders.MoveOrder(GetRayHitPoint());
            }
        }// Конец debug информации */                                                
    }
    
    // Получение точки клика на карте
    /// <summary>
    /// Get click point on map
    /// </summary>
    /// <returns>Vector3 point</returns>
    public static Vector3 GetRayHitPoint ()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            return hit.point;
        else
            return Vector3.zero;
    }
    /// <summary>
    /// Gets camera
    /// </summary>
    /// <returns>Camera</returns>
    public static Camera GetCamera()
    {
        return _camera;
    }
}