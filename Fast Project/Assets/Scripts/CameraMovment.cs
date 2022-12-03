using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    private Vector3 _startMousePosition;

    private void Update()
    {
        if (Input.GetMouseButton(2))
            transform.position += new Vector3(Time.deltaTime * (_startMousePosition.x - Input.mousePosition.x), 0, Time.deltaTime * (_startMousePosition.y - Input.mousePosition.y));


        _startMousePosition = Input.mousePosition;
    }
}
