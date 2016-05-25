using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour 
{
    public Transform cameraChild;
    public float cameraSpeed = 30f;
    private Vector3 currentPosition;

	// Use this for initialization
	void Awake () 
	{
        cameraChild = transform.FindChild("Main Camera");
        currentPosition = cameraChild.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Camera controls to move and zoom like in RTS
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward * Time.deltaTime * cameraSpeed;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back * Time.deltaTime * cameraSpeed;
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * Time.deltaTime * cameraSpeed;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * Time.deltaTime * cameraSpeed;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            Zoom(-1);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            Zoom(1);
	}
    /// <summary>
    /// Zoom camera
    /// </summary>
    /// <param name="y">Camera height</param>
    void Zoom(float y)
    {
        currentPosition = cameraChild.position;
        currentPosition.y += y;
        cameraChild.position = currentPosition;
    }
}