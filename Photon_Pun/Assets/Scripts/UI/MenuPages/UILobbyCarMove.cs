using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobbyCarMove : MonoBehaviour
{
    public Transform[] m_wayPoints;
    public int m_startIndex;
    public int m_currentAngle;

    float m_cooldown;
    float m_lerpStep;
    int m_currentIndex;
    int m_nextIndex;
    private void Start()
    {
        m_cooldown = 1;
        m_lerpStep = 0;
        m_currentIndex = m_startIndex;
        m_nextIndex = ((m_currentIndex + 1) >= m_wayPoints.Length) ? 0 : (m_currentIndex + 1);

        transform.position = m_wayPoints[m_currentIndex].position;
        transform.eulerAngles = new Vector3(0, 0, m_currentAngle);
    }

    private void Update()
    {
        if (m_cooldown > 0)
        {
            m_cooldown -= Time.deltaTime;
        }
        else
        {
            m_lerpStep += Time.deltaTime;
            transform.position = Vector3.Lerp(m_wayPoints[m_currentIndex].position, m_wayPoints[m_nextIndex].position, m_lerpStep);
            
            if (m_lerpStep >= 1)
            {
                m_cooldown = 0;
                m_lerpStep = 0;
                m_currentIndex = ((m_currentIndex + 1) >= m_wayPoints.Length) ? 0 : (m_currentIndex + 1);
                m_nextIndex = ((m_currentIndex + 1) >= m_wayPoints.Length) ? 0 : (m_currentIndex + 1);
                m_currentAngle = ((m_currentAngle + 90) == 360) ? 0 : (m_currentAngle + 90);

                transform.eulerAngles = new Vector3(0, 0, m_currentAngle);
            }
        }
    }
}
