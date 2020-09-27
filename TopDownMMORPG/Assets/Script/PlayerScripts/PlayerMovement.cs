using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    InputMaster Input;

    public float Speed;

    Rigidbody2D rb;

    PlayerManager PM;

    void Awake()
    {
        Input = new InputMaster();

        PM = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Input.Player.Enable();
    }
    void OnDisable()
    {
        Input.Player.Disable();
    }

    void Update()
    {
        //find the Input values
        /*
        float XMov = Input.GetAxisRaw("Horizontal");
        float YMov = Input.GetAxisRaw("Vertical");
        */

        Vector2 Mov = Input.Player.Movement.ReadValue<Vector2>();

        //convert the input values to a vector for movement
        Mov = new Vector2(Mov.x, Mov.y).normalized;

        //fix the speed for the Movement vector
        Mov *= Speed * Time.deltaTime;
        if (PM.CanMove)
        {

            //apply the movement via a rigidbody component
            rb.MovePosition(this.transform.position + (Vector3)Mov);
        }
        else
        {
            Mov = Vector2.zero;
        }

        //Activate Walking animation if moveing (Vector3 Mov X and Y is Not equals to zero)
        if(Mov != Vector2.zero)
        {
            GetComponent<PlayerAnimController>().Walking(true);
        }
        else
        {
            GetComponent<PlayerAnimController>().Walking(false);
        }
    }
}
