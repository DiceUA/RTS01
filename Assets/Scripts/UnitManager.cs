using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour 
{
    public List<GameObject> selectedUnits;
    public List<GameObject> unitsOnScreen;

    public List<GameObject> SelectedUnits 
    { 
        get 
        {
            return selectedUnits;
        } 
    }

    void Start ()
    {
        selectedUnits.Clear();
    }
    /// <summary>
    /// Select one unit and add it to List of selected units
    /// </summary>
    /// <param name="unit">GameObject</param>
    public void SelectUnitByClick (GameObject unit)
    {                
        selectedUnits.Clear();
        selectedUnits.Add(unit);    
    }
    /// <summary>
    /// Select multiple units and add it to List of selected units
    /// </summary>
    /// <param name="unit">GameObject</param>
    public void SelectMultipleUnitsByClick(GameObject unit)
    {
        if (!selectedUnits.Contains(unit))
            selectedUnits.Add(unit);
        else
            selectedUnits.Remove(unit);
    }
    /// <summary>
    /// Select one unit and add it to List of selected units
    /// use in Shift+Click mechanics
    /// </summary>
    /// <param name="unit">GameObject</param>
    public void SelectUnitsByMouse(GameObject unit)
    {
        if (!selectedUnits.Contains(unit))
            selectedUnits.Add(unit);
    }
    /// <summary>
    /// Deselect unit
    /// use in Shift+Click mechanics
    /// </summary>
    /// <param name="unit">GameObject</param>
    public void DeselectUnit(GameObject unit)
    {
        //unit.transform.FindChild("Selected").gameObject.SetActive(false); //stop show HP bars
        selectedUnits.Remove(unit);
    }
    /// <summary>
    /// Deselect all units    
    /// </summary>
    public void ClearSelection()
    {
        //ClearSelectedTag();
        selectedUnits.Clear();
    }
    /// <summary>
    /// Select enemy by click
    /// </summary>
    /// <param name="unit">GameObject</param>
    public void SelectEnemy(GameObject unit)
    {
        selectedUnits.Clear();
        selectedUnits.Add(unit);
    }


    #region Helper functions


    /// <summary>
    /// Stop showing HP bars
    /// </summary>
    void ClearSelectedTag()
    {
        foreach (GameObject unit in selectedUnits)
        {
            unit.transform.FindChild("Selected").gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// shows HP bar on every selected units
    /// </summary>
    public void ShowHPBar()
    {
        if(selectedUnits.Count > 0)
            foreach (GameObject unit in selectedUnits)
            {
                unit.transform.FindChild("Selected").gameObject.SetActive(true);
            }
    }

    // Draw unit name on top of unit
    void OnGUI()
    {
        if (selectedUnits.Count > 0)
        {            
            foreach (GameObject item in selectedUnits)
            {
                Vector3 unitScreenPosition = GameLogic.GetCamera().WorldToScreenPoint(item.transform.position);
                unitScreenPosition.y = MouseDrag.InvertScreenHeightlAxis(unitScreenPosition.y);
                Rect rect = new Rect(unitScreenPosition.x - 25, unitScreenPosition.y - 40, 65, 24);
                GUI.Label(rect, item.transform.name);
            }            
        }
    }

    #endregion
}