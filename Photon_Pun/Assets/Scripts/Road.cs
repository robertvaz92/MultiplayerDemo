using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Material m_material;
    Vector2 m_matOffset;
    GamePlayManager m_gamePlayManager;
    public void Initialize(GamePlayManager gamePlayManager)
    {
        m_matOffset = Vector2.zero;
        m_gamePlayManager = gamePlayManager;
    }

    // Update is called once per frame
    public void CustomUpdate()
    {
        m_matOffset.y += Time.deltaTime * m_gamePlayManager.m_speed;
        m_material.mainTextureOffset = m_matOffset;
        if (m_matOffset.y >= 1)
        {
            m_matOffset.y = 0;
        }
    }
}
