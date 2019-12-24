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
    [SerializeField]
    private float m_playerMoveSpeed = 0f;

    [SerializeField, Tooltip("This determines how close to zero the player's velocity needs to be to flip the sprite in the Sprite Renderer.")]
    private float m_turningSpriteFlipThreshold = 0f;
    #endregion------------

    #region Standard Local Member Variables (m_ == A local member of a class)
    private bool m_isfacingLeft;
    #endregion------------

    #region Component Variable Containers
    private Rigidbody2D m_playerRigidbody;
    private SpriteRenderer m_playerSpriteRenderer;
    #endregion------------

    /// <summary>
    /// An internal class that contains all movement related input fields required for this script.
    /// </summary>
    internal class InputListener
    {
        public float horizontalMoveInput;
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

        //If any input to the horizontal axis are detected, update the look direction to keep the sprite facing the last direction the player moved in
        if (!Mathf.Approximately(m_inputListener.horizontalMoveInput, 0.0f))
            UpdateLookDirection();

        //TODO: Debugging PlayerMovementHandler.Update()
        Debug.Log($"The player's current horizontal input is: {m_inputListener.horizontalMoveInput}\n" +
            $"The current value of the player's m_isFacingLeft variable is: {m_isfacingLeft}");
    }

    private void FixedUpdate()
    {
        //Apply horizontal player movement input.
        HorizontalMoveInputHandler();  
    }

    /// <summary>
    /// Used to Initialize any necessary component variables (e.g. Rigidbody2D, BoxCollider2D, etc.)
    /// </summary>
    private void InitializePlayerComponents()
    {
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    /// <summary>
    /// Listens for the input received from the player. This data is used by the HorizontalMoveInputHandler() to apply this to the movement of the player-character.
    /// </summary>
    private void HorizontalMoveInputListener()
    {
        m_inputListener.horizontalMoveInput = Input.GetAxisRaw("Horizontal");
    }

    /// <summary>
    /// Handles and applies the horizontal input received from the player
    /// </summary>
    private void HorizontalMoveInputHandler()
    {
        m_playerRigidbody.velocity = new Vector2(m_inputListener.horizontalMoveInput * m_playerMoveSpeed * Time.deltaTime, m_playerRigidbody.velocity.y);
    }

    /// <summary>
    /// Updates the direction the sprite should be facing based on the horizontal player input
    /// </summary>
    private void UpdateLookDirection()
    {
        if (m_inputListener.horizontalMoveInput > m_turningSpriteFlipThreshold)
        {
            m_playerSpriteRenderer.flipX = m_isfacingLeft = true;
            m_isfacingLeft = true;
        }
        else if (m_inputListener.horizontalMoveInput < -m_turningSpriteFlipThreshold)
        {
            m_playerSpriteRenderer.flipX = false;
            m_isfacingLeft = false;
        }
        else if (m_inputListener.horizontalMoveInput >= - m_turningSpriteFlipThreshold && m_inputListener.horizontalMoveInput <= m_turningSpriteFlipThreshold)
        {
            if (!m_isfacingLeft)
                m_playerSpriteRenderer.flipX = true;
            else
                m_playerSpriteRenderer.flipX = false;
        }
    }
}
