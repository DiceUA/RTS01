using UnityEngine;
using System.Collections;

public class GiveOrders : MonoBehaviour 
{
    private static UnitManager unitManager;
    private static bool first = true;
	void Start () 
	{
        //Find game object where unitManager.cs attached - GameObject.FindGameObjectWithTag("GameController").GetComponent<UnitManager>()
        //GiveOrders.cs attached to GameController also, so we can write simplier gameObject.GetComponent<UnitManager>
        unitManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UnitManager>();
	}	
	void Update () 
	{
        if (Input.GetMouseButtonDown(0))
            //Debug.Log("Selected Units " + unitManager.SelectedUnits);
            Debug.Log("Ray point= " + GameLogic.GetRayHitPoint());
	}
    /// <summary>
    /// Make move order to unit
    /// Unit will move to point where player clicked
    /// </summary>
    /// <param name="rayHitPoint">Vector3 point</param>
    public static void MoveOrder(Vector3 rayHitPoint)
    {
        
        foreach (GameObject unit in unitManager.SelectedUnits)
        {
            if(first)
            {
                Debug.Log("Orders for " + unit.name + " Confirmed. Point = " + rayHitPoint);
                unit.SendMessage("UnitMovement", rayHitPoint);
                first = false;                       
            }
            else
            {
                Vector3 nearPosition = rayHitPoint;
                nearPosition.x += Random.Range(-3, 3);
                nearPosition.z += Random.Range(-3, 3);
                Debug.Log("Orders for " + unit.name + " Confirmed. Point = " + nearPosition);
                unit.SendMessage("UnitMovement", nearPosition);
            }            
        }
        first = true;
    }

}