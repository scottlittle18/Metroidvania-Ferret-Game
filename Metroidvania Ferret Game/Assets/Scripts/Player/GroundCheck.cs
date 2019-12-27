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

    private bool m_isGrounded;
    public bool IsGrounded
    {
        get { return m_isGrounded; }
        private set { m_isGrounded = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(this.transform.position, groundCheckRadius, whatIsGround);

        //TODO: Debug GroundCheck.cs
        Debug.Log($"IsGrounded == {IsGrounded}");
    }
}
