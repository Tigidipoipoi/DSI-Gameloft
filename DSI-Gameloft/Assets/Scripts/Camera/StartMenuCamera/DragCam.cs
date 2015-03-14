using UnityEngine;
using System.Collections;

public class DragCam : MonoBehaviour {

    public float speed = 0.1F;

    private Vector3 lastMousePosition;

    private Camera maincamera;

    public Transform bas;
    public Transform haut;

    void Start()
    {
        maincamera = Camera.main;
    }

    void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Vector3 distance = -(Input.mousePosition - lastMousePosition)*speed;
        maincamera.transform.position = new Vector3(maincamera.transform.position.x, maincamera.transform.position.y + distance.y * Time.deltaTime, maincamera.transform.position.z);

        if (maincamera.transform.position.y < bas.transform.position.y)
        {
            maincamera.transform.position = new Vector3(maincamera.transform.position.x, bas.transform.position.y, maincamera.transform.position.z);
        }
        if (maincamera.transform.position.y > haut.transform.position.y)
        {
            maincamera.transform.position = new Vector3(maincamera.transform.position.x, haut.transform.position.y, maincamera.transform.position.z);
        }
        
    }


}
