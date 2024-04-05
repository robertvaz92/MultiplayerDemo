using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCircle : MonoBehaviour
{
    public bool m_shouldLoadBySteps;
    public float m_stepAngle;
    public float m_stepInterval;


    float m_currentAngle = 0;
    Vector3 m_eulerAngles;
    float m_cooldown;
    void Start()
    {
        m_eulerAngles = Vector3.zero;
        m_cooldown = m_stepInterval;
    }

    void Update()
    {
        if (m_shouldLoadBySteps)
        {
            if (m_cooldown > 0)
            {
                m_cooldown -= Time.deltaTime;
            }
            else
            {
                m_currentAngle += m_stepAngle;
                if (m_currentAngle > 360)
                {
                    m_currentAngle = m_currentAngle - 360;
                }
                m_eulerAngles.z = m_currentAngle;
                transform.rotation = Quaternion.Euler(m_eulerAngles);
                m_cooldown = m_stepInterval;
            }
        }
    }
}