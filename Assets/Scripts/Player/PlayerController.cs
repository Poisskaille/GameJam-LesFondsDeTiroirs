using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float runspeed;
    float horizontalMove;
    float verticalMove;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystick.Horizontal * runspeed ;
        verticalMove = joystick.Vertical * runspeed ;
        rb.velocity = new Vector3(horizontalMove, rb.velocity.y, verticalMove);

    }

}
