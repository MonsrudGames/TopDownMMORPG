using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

public class CameraMovement : MonoBehaviour
{
    private Vector3 DragOrigin, LastFrameDragOrigin;

    PixelPerfectCamera PPC;

    bool FollowPlayer;

    PlayerManager PM;

    private void Start()
    {
        PM = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        PPC = GetComponent<PixelPerfectCamera>();

        this.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
    }

    void Update()
    {
        //check if Camera Can Move. this is disabled if inventory or pause menu is open
        if (PM.CameraCanMove)
        {
            //Move Camera
            if (Input.GetMouseButtonDown(1))
            {
                FollowPlayer = false;
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
                FollowPlayer = !FollowPlayer;
                this.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
            }

            if(FollowPlayer)
            {
                this.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -10f);
            }

            this.transform.position -= Camera.main.ScreenToWorldPoint(DragOrigin) - Camera.main.ScreenToWorldPoint(LastFrameDragOrigin);

            LastFrameDragOrigin = DragOrigin;

            //Camera Zoom
            float MouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");

            if (MouseWheel < 0f && PPC.assetsPPU > 15)
            {
                PPC.assetsPPU += (int)(MouseWheel * PPC.assetsPPU);
            }
            if (MouseWheel > 0f && PPC.assetsPPU < 75)
            {
                PPC.assetsPPU += (int)(MouseWheel * PPC.assetsPPU);
            }
        }
    }
}