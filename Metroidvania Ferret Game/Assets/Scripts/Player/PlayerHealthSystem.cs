using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    private int m_totalHealth;
    private int m_currentPlayerHealth;

    private Checkpoint m_currentCheckpoint;

    private Transform m_currentCheckpointTransform, m_spawnPointTransform;

    /// <summary>
    /// This is the checkpoint that the player will return to if they die.
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
                m_currentCheckpoint = value;
                m_currentCheckpoint.IsActivated = true;
            }
            else
            {
                m_currentCheckpoint.IsActivated = false;
                m_currentCheckpoint = value;
                m_currentCheckpoint.IsActivated = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
