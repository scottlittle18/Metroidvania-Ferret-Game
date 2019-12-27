using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for and Handles movement related input (e.g. Running, Dashing, etc.)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementHandler : MonoBehaviour
{
    #region Serialized Fields
    [Header("Player Movement Settings")]

    [SerializeField, Tooltip("How fast will the player be able to run around?")]
    private float m_playerMoveSpeed = 0f;

    [SerializeField, Tooltip("How fast will the player be able to jump upward?")]
    private float m_playerJumpSpeed = 0f;

    [SerializeField, Tooltip("Adjusts the length of time that the jump input is accepted for. (This is modeled after Mario's jump mechanic).")]
    private float m_jumpLength = 0;

    [SerializeField, Tooltip("This determines how close to zero the player's velocity needs to be to flip the sprite in the Sprite Renderer.")]
    private float m_turningSpriteFlipThreshold = 0f;
    #endregion------------

    #region Standard Local Member Variables (m_ == A local member of a class)
    private bool m_isfacingLeft;
    private bool m_canJump;
    #endregion------------

    #region Component Variable Containers
    private Rigidbody2D m_playerRigidbody;
    private SpriteRenderer m_playerSpriteRenderer;
    private GroundCheck m_groundCheck;
    #endregion------------

    /// <summary>
    /// An internal class that contains all movement related input fields required for this script.
    /// </summary>
    internal class InputListener
    {
        public float m_horizontalMoveInput;

        public bool m_isJumping;
    }

    private InputListener m_inputListener = new InputListener();

    // Start is called before the first frame update
    private void Start()
    {
        //Set up all necessary component variables
        InitializePlayerComponents();
    }

    // Update is called once per frame
    private void Update()
    {
        //--Listeners--
        HorizontalMoveInputListener();
        JumpInputListener();

        //If any input to the horizontal axis are detected, update the look direction to keep the sprite facing the last direction the player moved in
        if (!Mathf.Approximately(m_inputListener.m_horizontalMoveInput, 0.0f))
            UpdateLookDirection();

        //TODO: Debugging PlayerMovementHandler.Update()
        Debug.Log($"The player's current horizontal input is: {m_inputListener.m_horizontalMoveInput}\n" +
            $"The current value of the player's m_isFacingLeft variable is: {m_isfacingLeft}");
    }

    private void FixedUpdate()
    {
        //Apply horizontal player movement input.
        HorizontalMoveInputHandler();
        JumpInputHandler();
    }

    /// <summary>
    /// Used to Initialize any necessary component variables (e.g. Rigidbody2D, BoxCollider2D, etc.)
    /// </summary>
    private void InitializePlayerComponents()
    {
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_playerSpriteRenderer = GetComponent<SpriteRenderer>();
        m_groundCheck = GetComponentInChildren<GroundCheck>();
    }

    #region _________________________________________________________________LISTENERS______________________________
    /// <summary>
    /// Listens for the input received from the player. This data is used by the HorizontalMoveInputHandler() to apply this to the movement of the player-character.
    /// </summary>
    private void HorizontalMoveInputListener()
    {
        m_inputListener.m_horizontalMoveInput = Input.GetAxisRaw("Horizontal");
    }

    /// <summary>
    /// Listens for jump input.
    /// </summary>
    private void JumpInputListener()
    {
        m_inputListener.m_isJumping = Input.GetButton("Jump");
    }
    #endregion-------------------------------------------------------------------------------------------------------

    #region ______________________________________________________________________HANDLERS__________________________
    /// <summary>
    /// Handles and applies the horizontal input received from the player
    /// </summary>
    private void HorizontalMoveInputHandler()
    {
        m_playerRigidbody.velocity = new Vector2(m_inputListener.m_horizontalMoveInput * m_playerMoveSpeed * Time.deltaTime, m_playerRigidbody.velocity.y);
    }

    /// <summary>
    /// Limits the length of time the player will be able to jump for.
    /// </summary>
    /// <returns>Waits for the given length of time [Adjustable In-Editor]</returns>
    private IEnumerator JumpTimeLimiter()
    {
        yield return new WaitForSecondsRealtime(m_jumpLength);
    }

    /// <summary>
    /// Handles and applies the jump input received from the player
    /// </summary>
    private void JumpInputHandler()
    {
        // If the player is trying to jump
        if (m_groundCheck.IsGrounded && m_inputListener.m_isJumping)
        {
            //TODO: Debug Jump Input
            Debug.Log($"m_groundCheck.IsGrounded == {m_groundCheck.IsGrounded}");

            m_playerRigidbody.velocity = new Vector2(m_playerRigidbody.velocity.x, m_playerJumpSpeed * Time.deltaTime);
        }
    }
    
    /// <summary>
    /// Updates the direction the sprite should be facing based on the horizontal player input
    /// </summary>
    private void UpdateLookDirection()
    {
        if (m_inputListener.m_horizontalMoveInput > m_turningSpriteFlipThreshold)
        {
            m_playerSpriteRenderer.flipX = m_isfacingLeft = true;
            m_isfacingLeft = true;
        }
        else if (m_inputListener.m_horizontalMoveInput < -m_turningSpriteFlipThreshold)
        {
            m_playerSpriteRenderer.flipX = false;
            m_isfacingLeft = false;
        }
        else if (m_inputListener.m_horizontalMoveInput >= - m_turningSpriteFlipThreshold && m_inputListener.m_horizontalMoveInput <= m_turningSpriteFlipThreshold)
        {
            if (!m_isfacingLeft)
                m_playerSpriteRenderer.flipX = true;
            else
                m_playerSpriteRenderer.flipX = false;
        }
    }
    #endregion-------------------------------------------------------------------------------------------------------
}



/* Psuedo-Code
 * 
 *      Jump Timer:
 *          
 *          if (!groundCheck.IsGrounded && m_inputListener.m_isJumping)
 *          {
 *              StartCoroutine(JumpTimeLimiter);
 *          }
 *          else
 *          {
 *              StopCoroutine(JumpTimeLimiter);
 *          }
 */