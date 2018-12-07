using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

public class CameraMovement : MonoBehaviour
{
    private Vector3 DragOrigin, LastFrameDragOrigin;

    PixelPerfectCamera PPC;

    private void Start()
    {
        PPC = GetComponent<PixelPerfectCamera>();
    }

    void Update()
    {
        //Move Camera
        if (Input.GetMouseButtonDown(1))
        {
            DragOrigin = LastFrameDragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            DragOrigin = Input.mousePosition;
        }
        if (!Input.GetMouseButton(1))
        {
            DragOrigin = LastFrameDragOrigin = Vector3.zero;
        }

        this.transform.position -= Camera.main.ScreenToWorldPoint(DragOrigin) - Camera.main.ScreenToWorldPoint(LastFrameDragOrigin);

        LastFrameDragOrigin = DragOrigin;
    }
}