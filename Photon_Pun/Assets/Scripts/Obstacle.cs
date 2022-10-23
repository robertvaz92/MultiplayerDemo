using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Renderer m_renderer;
    public Color m_color1;
    public Color m_color2;

    ObstacleManager m_manager;
    Transform m_objTrans;
    Vector3 m_pos;
    bool m_isActivated = false;

    Material m_obsMaterial;
    bool m_colorUpdateBool;
    float m_colorUpdateLerpStep;

    public void Initialize(ObstacleManager manager)
    {
        m_manager = manager;
        m_objTrans = transform;
        m_obsMaterial = m_renderer.material;
        DisableObject();
    }

    public void Activate()
    {
        m_isActivated = true;
        Quaternion q = m_objTrans.rotation;
        q.z = 180;
        m_objTrans.rotation = q;
        EnableObject();

        m_colorUpdateBool = true;
        m_colorUpdateLerpStep = 0;
    }

    public void CustomUpdate()
    {
        if (m_isActivated)
        {
            m_pos = m_objTrans.position;
            m_pos.y -= GameDataManager.m_instance.m_obstacleMoveSpeed * Time.deltaTime;
            m_objTrans.position = m_pos;

            if (m_pos.y < -6f)
            {
                m_isActivated = false;
                m_manager.PoolObstacle(this);
                DisableObject();
            }
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        if (m_colorUpdateBool)
        {
            m_colorUpdateLerpStep += Time.deltaTime * 10;
            if (m_colorUpdateLerpStep > 1)
            {
                m_colorUpdateBool = false;
                m_colorUpdateLerpStep = 1;
            }
        }
        else
        {
            m_colorUpdateLerpStep -= Time.deltaTime * 10;
            if (m_colorUpdateLerpStep < 0)
            {
                m_colorUpdateBool = true;
                m_colorUpdateLerpStep = 0;
            }
        }
        m_obsMaterial.color = Color.Lerp(m_color1, m_color2, m_colorUpdateLerpStep);
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