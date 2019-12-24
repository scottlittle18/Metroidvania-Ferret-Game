using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to detect if the attached character is on the ground
/// </summary>
[RequireComponent(typeof(Collider2D))] //TODO: --Polish-- Make this call for the proper collider
public class GroundCheck : MonoBehaviour
{
    private BoxCollider2D m_groundCheckCollider;

    private bool m_isGrounded;
    public bool IsGrounded
    {
        get { return m_isGrounded; }
        private set { m_isGrounded = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_groundCheckCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player is standing on anything then they are grounded
        IsGrounded = true;
    }
}
