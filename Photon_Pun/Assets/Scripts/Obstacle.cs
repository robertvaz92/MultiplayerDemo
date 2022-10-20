using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    ObstacleManager m_manager;
    Transform m_objTrans;
    Vector3 m_pos;
    bool m_isActivated = false;

    public void Initialize(ObstacleManager manager)
    {
        m_manager = manager;
        m_objTrans = transform;
        DisableObject();
    }

    public void Activate()
    {
        m_isActivated = true;
        EnableObject();
    }

    public void CustomUpdate()
    {
        if (m_isActivated)
        {
            m_pos = m_objTrans.position;
            m_pos.y -= m_manager.m_manager.m_obsSpeed * Time.deltaTime;
            m_objTrans.position = m_pos;

            if (m_pos.y < -5f)
            {
                m_isActivated = false;
                m_manager.PoolObstacle(this);
                DisableObject();
            }
        }
    }

    void EnableObject()
    {
        gameObject.SetActive(true);
    }
    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}