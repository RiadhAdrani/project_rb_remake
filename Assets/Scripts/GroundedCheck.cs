using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundMask;

    private void Update()
    {
        isGrounded =  Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);
    }
}
