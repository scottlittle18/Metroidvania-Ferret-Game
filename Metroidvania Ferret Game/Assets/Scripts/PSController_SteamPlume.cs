using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PURPOSE:
///     This script is meant to be attached to the steam plume hazards/platform ascenders.
///     
/// Implementation Steps:
///     1. Create a particle system in Unity
///     2. Attach to the particle system you want to create a delay for.
///     3. Specify in Unity how long you want to delay the particle emission for in the PlumeCooldownTime field in the inspector.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class PSController_SteamPlume : MonoBehaviour
{
    [SerializeField, Tooltip("How long to wait between plumes.")]
    private float m_plumeCooldownTime = 0.0f;

    private bool m_canPlay;
    
    private ParticleSystem m_steamPlumeSystem;

    private IEnumerator SteamPlumeCooldown()
    {
        // Wait for the particle system to finish playing
        yield return new WaitForSecondsRealtime(m_steamPlumeSystem.main.duration);

        // Set the particle system to no longer be able to play (gets shutoff by local_isEmitting)
        m_canPlay = false;

        // Initiate delay between plumes
        yield return new WaitForSecondsRealtime(m_plumeCooldownTime);

        // Set the particle system to be able to be played again
        m_canPlay = true;
    }

    private void Awake()
    {
        m_steamPlumeSystem = GetComponent<ParticleSystem>();

        // Sets the default value of m_canPlay to be equal to the state of the PlayOnAwake toggle in the particle system
        m_canPlay = m_steamPlumeSystem.main.playOnAwake;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Debug Method
        Debugging();
        
        // 'var' is used due to the absence of an Emission object type, and I really don't know what else to use 
        var lm_isEmitting = m_steamPlumeSystem.emission;
        // If the particle system can play it will, otherwise it will turn off the emission portion of the particle system component
        lm_isEmitting.enabled = m_canPlay;

        // Play the particle system
        if (lm_isEmitting.enabled == true && m_steamPlumeSystem.isEmitting == false)
        {
            m_steamPlumeSystem.Play();
        }

        // If the particle system can be played, which means it is playing (grammatically "canPlay" seemed to make sense in the most contexts when compared to "isPlaying"), start the SteamPlumeCooldown() coroutine
        if (m_canPlay)
        {
            StartCoroutine(SteamPlumeCooldown());
        }
    }

    /// <summary>
    /// TODO: To be removed later, but is currently used for general debugging on this script
    /// </summary>
    private void Debugging()
    {
        /* Commented out temporarily to keep the Unity Console clean.
         * 
        Debug.Log($"m_canPlay == {m_canPlay}");
        Debug.Log($"Emission.Enabled == {m_steamPlumeSystem.emission.enabled}");
        */
    }
}
