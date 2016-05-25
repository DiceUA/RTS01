using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour 
{
    private Camera _camera;
    private Vector3 mouseDownPosition, mouseCurrentPosition, startClick = -Vector3.one;
    public GUIStyle boxTexture;
    private float boxLeft, boxTop, boxWidth, boxHeight;
    public static Vector2 boxStart, boxFinish;
    public static Rect selection;
    private static bool isDragging;

    void Start ()
    {
        _camera = GameLogic.GetCamera();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!isDragging)
            {
                if (IsUserDragging(startClick, Input.mousePosition))
                {
                    isDragging = true;
                    Debug.Log("Is dragging");                    
                }                                    
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            startClick = -Vector3.one; 
            //Debug.Log("Stopped Dragging");
        }

        DrawRectangle();        
    }

    void OnGUI()
    {        
        if (startClick != -Vector3.one)
            GUI.Box(selection, "", boxTexture);
        GUI.Box(new Rect(10, 10, 300, 25), "" + selection); //Debug rect
    }
    /// <summary>
    /// Detect mouse position on screen
    /// </summary>
    void DetectMousePositions ()
    {
        if (Input.GetMouseButtonDown(0))
            mouseDownPosition = GameLogic.GetRayHitPoint();
        if (Input.GetMouseButton(0))
            mouseCurrentPosition = GameLogic.GetRayHitPoint();
    }
    /// <summary>
    /// just variant of box drawer
    /// </summary>
    public void DetectRectangle()
    {
        if (_camera == null)
            _camera = GameLogic.GetCamera();
        DetectMousePositions();

        boxWidth = _camera.WorldToScreenPoint(mouseDownPosition).x - _camera.WorldToScreenPoint(mouseCurrentPosition).x;
        boxHeight = _camera.WorldToScreenPoint(mouseDownPosition).y - _camera.WorldToScreenPoint(mouseCurrentPosition).y;
        boxLeft = Input.mousePosition.x;
        boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;

        if (boxWidth > 0 && boxHeight > 0)
            boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + boxHeight);
        else if (boxWidth > 0 && boxHeight < 0)
            boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        else if (boxWidth < 0 && boxHeight > 0)
            boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);
        else if (boxWidth < 0 && boxHeight < 0)
            boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y);

        boxFinish = new Vector2(boxStart.x + Unsigned(boxWidth), boxStart.y - Unsigned(boxHeight));
        selection = new Rect(boxLeft, boxTop, boxWidth, boxHeight);
    }
    /// <summary>
    /// Draw rectangle so player can see what he selects
    /// </summary>
    void DrawRectangle()
    {
        if (Input.GetMouseButtonDown(0))
            startClick = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
            startClick = -Vector3.one;        
        if (Input.GetMouseButton(0))
        {
            selection = new Rect(startClick.x, InvertScreenHeightlAxis(startClick.y), Input.mousePosition.x - startClick.x, InvertScreenHeightlAxis(Input.mousePosition.y) - InvertScreenHeightlAxis(startClick.y));

            if (selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.width;
            }
            if (selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }
        }            
    }
    
    #region Helper funtions

    /// <summary>
    /// Invert screen height axis
    /// </summary>
    /// <param name="yAxis"></param>
    /// <returns></returns>
    public static float InvertScreenHeightlAxis(float yAxis)
    {
        return Screen.height - yAxis;
    }
    /// <summary>
    /// Make float unsigned
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    float Unsigned(float number)
    {
        if (number < 0)
            number *= -1;
        return number;
    }
    /// <summary>
    /// Return if player is dragging now
    /// </summary>
    /// <returns></returns>
    public static bool IsDragging()
    {
        return isDragging;
    }


    /// <summary>
    /// detect if user click or drag
    /// </summary>
    /// <returns></returns>
    public bool IsUserClicked()
    {
        float clickZone = 1.2f;
        if ((startClick.x < GameLogic.GetRayHitPoint().x + clickZone && startClick.x > GameLogic.GetRayHitPoint().x - clickZone) &&
           (startClick.y < GameLogic.GetRayHitPoint().y + clickZone && startClick.y > GameLogic.GetRayHitPoint().y - clickZone) &&
           (startClick.x < GameLogic.GetRayHitPoint().z + clickZone && startClick.z > GameLogic.GetRayHitPoint().z - clickZone))
            return true;
        else
            return false;
    }
    /// <summary>
    /// Detect if user dragging or just missedclick
    /// </summary>
    /// <param name="dragStart">Vector2</param>
    /// <param name="newPoint">Vector2</param>
    /// <returns></returns>
    public bool IsUserDragging(Vector2 dragStart, Vector2 newPoint)
    {
        float clickZone = 1.2f;
        if ((newPoint.x > dragStart.x + clickZone || newPoint.x < dragStart.x - clickZone) ||
            (newPoint.y > dragStart.y + clickZone || newPoint.y < dragStart.y - clickZone))
            return true;
        else
            return false;
    }

    #endregion
}