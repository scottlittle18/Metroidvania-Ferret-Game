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
    private float playerMoveSpeed;
    #endregion

    private Rigidbody2D playerRigidbody;

    /// <summary>
    /// Contains all movement related input required for this script.
    /// </summary>
    internal class InputListener
    {
        public float horizontalMoveInput;
    }

    private InputListener inputListener = new InputListener();

    // Start is called before the first frame update
    private void Start()
    {
        //Set up all necessary component variables
        InitializePlayerComponents();

    }

    // Update is called once per frame
    private void Update()
    {
        HorizontalMoveInputListener();
    }

    private void FixedUpdate()
    {
        //Apply horizontal player movement input.
        HorizontalMoveInputHandler();        
    }

    /// <summary>
    /// Listens for the input received from the player. This data is used by the HorizontalMoveInputHandler() to apply this to the movement of the player-character.
    /// </summary>
    private void HorizontalMoveInputListener()
    {
        inputListener.horizontalMoveInput = Input.GetAxisRaw("Horizontal");
    }
    
    /// <summary>
    /// Used to Initialize any necessary component variables (e.g. Rigidbody2D, BoxCollider2D, etc.)
    /// </summary>
    private void InitializePlayerComponents()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Handles and applies the horizontal input received from the player
    /// </summary>
    private void HorizontalMoveInputHandler()
    {
        playerRigidbody.velocity = new Vector2(inputListener.horizontalMoveInput * playerMoveSpeed * Time.deltaTime, playerRigidbody.velocity.y);
    }
}
