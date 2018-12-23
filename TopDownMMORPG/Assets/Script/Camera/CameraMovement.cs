using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

public class CameraMovement : MonoBehaviour
{
    private Vector3 DragOrigin, LastFrameDragOrigin;

    // PixelPerfectCamera PPC;

    private void Start()
    {
        // PPC = GetComponent<PixelPerfectCamera>();

        this.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            this.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
        }

        this.transform.position -= Camera.main.ScreenToWorldPoint(DragOrigin) - Camera.main.ScreenToWorldPoint(LastFrameDragOrigin);

        LastFrameDragOrigin = DragOrigin;
    }
}