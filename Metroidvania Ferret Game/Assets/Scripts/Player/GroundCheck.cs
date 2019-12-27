using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to detect if the attached character is on the ground
/// </summary>
public class GroundCheck : MonoBehaviour
{
    [SerializeField, Tooltip("Adjusts the radius for the ground check object.")]
    private float groundCheckRadius;

    [SerializeField, Tooltip("Used to specify which layer mask the ground check will look for.")]
    private LayerMask whatIsGround;

    //This allows the IsGrounded variable to be read from the associated object's movement script.
    private bool m_isGrounded;
    public bool IsGrounded
    {
        get { return m_isGrounded; }
        private set { m_isGrounded = value; }
    }

    private void Update()
    {
        //IsGrounded == true if the player is on an object in the "Ground" LayerMask.
        IsGrounded = Physics2D.OverlapCircle(this.transform.position, groundCheckRadius, whatIsGround);
    }
}
