using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSController_SteamPlume : MonoBehaviour
{
    [SerializeField, Tooltip("How long to wait between plumes.")]
    private float m_plumeCooldownTime = 0.0f;

    private bool m_canPlay;
    
    private ParticleSystem m_steamPlumeSystem;

    private IEnumerator SteamPlumeCooldown()
    {
        //m_steamPlumeSystem.Play();
        yield return new WaitForSecondsRealtime(m_steamPlumeSystem.main.duration);
        m_canPlay = false;
        yield return new WaitForSecondsRealtime(m_plumeCooldownTime);
        m_canPlay = true;
    }

    private void Awake()
    {
        m_steamPlumeSystem = GetComponent<ParticleSystem>();
        m_canPlay = m_steamPlumeSystem.main.playOnAwake;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Debug Method
        Debugging();

        var m_isEmitting = m_steamPlumeSystem.emission;
        m_isEmitting.enabled = m_canPlay;

        if (m_isEmitting.enabled == true && m_steamPlumeSystem.isEmitting == false)
        {
            m_steamPlumeSystem.Play();
        }

        if (m_canPlay)
        {
            StartCoroutine(SteamPlumeCooldown());
        }

        //if (m_canPlay)
        //{
        //    m_steamPlumeSystem.Play();
        //    StartCoroutine(SteamPlumeCooldown());
        //}

        //if (!m_canPlay)
        //{
        //    m_steamPlumeSystem.Stop();
        //}
    }

    private void Debugging()
    {
        //Debug.Log($"m_isEmitting == {m_isEmitting}");
        Debug.Log($"m_canPlay == {m_canPlay}");
        Debug.Log($"Emission.Enabled == {m_steamPlumeSystem.emission.enabled}");
    }
}
