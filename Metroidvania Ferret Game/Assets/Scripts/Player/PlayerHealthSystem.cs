using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Purpose:
///     Handles the Health of the Player Character as well as their respawn
/// </summary>
public class PlayerHealthSystem : MonoBehaviour, IDamagable
{
    /// <summary>
    /// The maximum amount of health the player can have when their health is full.
    /// </summary>
    [SerializeField, Tooltip("The maximum amount of health the player can have when their health is full.")]
    private int m_maxPlayerHealth;

    /// <summary>
    /// How much health does the player have at this point in time?
    /// </summary>
    private int m_currentPlayerHealth;

    private bool m_isAlive = true;

    public bool IsAlive
    {
        get { return m_isAlive; }
        private set
        {
            m_isAlive = m_currentPlayerHealth > 0 ? true : false;
        }
    }

    /// <summary>
    /// The position the player will start the level at.
    /// </summary>
    private Transform m_levelStartPointTransform;

    /// <summary>
    /// The position of the current Checkpoint.
    /// </summary>
    private Transform m_currentCheckpointTransform;

    /// <summary>
    /// *USE CurrentCheckpoint PROPERTY INSTEAD*
    /// </summary>
    private Checkpoint m_currentCheckpoint;
    
    /// <summary>
    /// The Checkpoint that the player will respawn at upon death.
    /// </summary>
    public Checkpoint CurrentCheckpoint
    {
        get
        {
            return m_currentCheckpoint;
        }
        set
        {
            if (m_currentCheckpoint == null)
            {
                //If no checkpoint has been reached yet, then there is no need to deactivate an old checkpoint
                m_currentCheckpoint = value;
                m_currentCheckpoint.IsActivated = true;
            }
            else
            {
                //Deactivate old checkpoint
                m_currentCheckpoint.IsActivated = false;

                //Set to the new checkpoint
                m_currentCheckpoint = value;

                //Activate new checkpoint
                m_currentCheckpoint.IsActivated = true;
            }
        }
    }

    private void Awake()
    {
        //Start player with a full health bar
        m_currentPlayerHealth = m_maxPlayerHealth;

        // TODO: Debug to display player's current health
        Debug.Log($"The player's current health is: {m_currentPlayerHealth}");

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");

        if (spawnPoint != null)
        {
            m_levelStartPointTransform = GetComponent<Transform>();

            //Sets the player's position to be at the Spawn Point 
            m_levelStartPointTransform.transform.position = spawnPoint.transform.position;
        }
        else
        {
            //TODO: Debug log for checking if a spawn point exists in the given level
            Debug.Log("A spawn point was not detected...");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentPlayerHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damageRecieved)
    {
        //TODO: Debug PlayerHealthSystem TakeDamage()
        Debug.Log($"Player has taken damage!");
        
        m_currentPlayerHealth -= damageRecieved;

        Debug.Log($"Player now has {m_currentPlayerHealth}HP");
    }

    public void Die()
    {
        //TODO: Debug PlayerDeath
        Debug.Log("Player is dead.");
    }
}
