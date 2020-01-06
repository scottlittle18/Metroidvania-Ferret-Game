using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Used to handle the checkpoint system.
/// </summary>
public class Checkpoint : MonoBehaviour
{    
    private bool m_isActivated;   
    private Animator m_anim;
    private PlayerHealthSystem m_playerHealth;
    /// <summary>
    /// Property Used to Activate or Deactivate checkpoints
    /// </summary>
    public bool IsActivated
    {
        get
        {
            return m_isActivated;
        }
        set
        {
            m_isActivated = value;
            UpdateAnimation();
        }
    }

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    private void Start()
    {
        IsActivated = false;        
    }

    /// <summary>
    /// Update the animator of the checkpoint.
    /// </summary>
    private void UpdateAnimation()
    {
        m_anim.SetBool("isActivated", IsActivated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_playerHealth = collision.GetComponent<PlayerHealthSystem>();
            m_playerHealth.CurrentCheckpoint = this;
        }
    }
}
