using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Purpose:
///     This script is meant to be attached to ascending platforms to keep them from flying off, but also allows them to be heavy enough to control the rate in which they rise.
///     
/// Note:
///     *This requires a Rigidbody2D component in order to allow it to be effected by gravity applied by the Unity Physics System*
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class RisingPlatformVelocityLimiter : MonoBehaviour
{
    [SerializeField, Tooltip("What is the maximum speed that this platform will be able to rise")]
    private float m_maxVelocity;

    private Rigidbody2D m_platformBody;

    // Start is called before the first frame update
    void Start()
    {
        m_platformBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 clampedVelocity = m_platformBody.velocity;
        clampedVelocity.y = Mathf.Clamp(m_platformBody.velocity.y, Mathf.NegativeInfinity, m_maxVelocity);
        m_platformBody.velocity = clampedVelocity;
    }
}
