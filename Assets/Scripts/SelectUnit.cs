using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// This script allow unit selection by Click or Draggable selection. 
// Attach it to unit.

public class SelectUnit : MonoBehaviour 
{

    private UnitManager unitManager;
    private bool selected = false;

    void Awake ()
    {
        //Find game object where UnitManager.cs attached - GameObject.FindGameObjectWithTag("GameController").GetComponent<UnitManager>()
        //SelectUnit.cs attached to Unit so we need this code
        unitManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UnitManager>();
        if (unitManager != null)
            Debug.Log("SelectUnit.cs unitManager initialised.");
    }

    public void Update()
    {
        if (!MouseDrag.IsDragging())
            Clicked();
        else
            SelectUnitsInDrag();
    }
    /// <summary>
    /// Select all units that catched in drag rectangle
    /// </summary>
    void SelectUnitsInDrag()
    {
        Vector3 unitScreenPosition = GameLogic.GetCamera().WorldToScreenPoint(transform.position);
        unitScreenPosition.y = MouseDrag.InvertScreenHeightlAxis(unitScreenPosition.y);
        selected = MouseDrag.selection.Contains(unitScreenPosition);
        if (selected)
        {
            unitManager.SelectUnitsByMouse(gameObject);
            unitManager.ShowHPBar();
        }            
        else
        {
            unitManager.DeselectUnit(gameObject);
        }
    }



    #region Helper functions

    //
    //if you have child object that highlight unit you will need this code to cancel highlight
    //public static void DeselectSelectedGameObject ()
    //{
    //    if (currentlySelected.Count > 0)
    //    {
    //        foreach (GameObject item in currentlySelected)
    //        {
    //            item.transform.FindChild("Selected").gameObject.SetActive(false);
    //        }
    //        currentlySelected.Clear();
    //    }
    //}

    /// <summary>
    /// Determine if player holding Shift
    /// </summary>
    /// <returns></returns>
    public static bool ShiftKeyDown()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            return true;
        else
            return false;
    }

    
    /// <summary>
    /// Click on unit or ground logic
    /// </summary>
    void Clicked()
    {
        Ray ray = GameLogic.GetCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) == true)
            {
                if (hit.transform.gameObject.tag == "Enemy")
                    unitManager.SelectEnemy(hit.transform.gameObject);
                if (hit.transform.gameObject.tag == "Terrain")
                {
                    if (!ShiftKeyDown())
                        unitManager.ClearSelection();
                }
                else
                {
                    if (!ShiftKeyDown())
                    { unitManager.SelectUnitByClick(hit.transform.gameObject); Debug.Log("" + hit.transform.gameObject); }
                    if (ShiftKeyDown())
                        unitManager.SelectMultipleUnitsByClick(hit.transform.gameObject);
                }
            }
        }
    } 
    #endregion
}