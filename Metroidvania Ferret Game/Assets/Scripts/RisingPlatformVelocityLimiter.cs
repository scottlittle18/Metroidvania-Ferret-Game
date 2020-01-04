using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatformVelocityLimiter : MonoBehaviour
{
    [SerializeField]
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
