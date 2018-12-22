using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float Speed;

    Rigidbody2D rb;

    public bool CanMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //find the Input values
        float XMov = Input.GetAxisRaw("Horizontal");
        float YMov = Input.GetAxisRaw("Vertical");

        //convert the input values to a vector for movement
        Vector2 Mov = new Vector2(XMov, YMov).normalized;

        //fix the speed for the Movement vector
        Mov *= Speed * Time.deltaTime;
        if (CanMove)
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
