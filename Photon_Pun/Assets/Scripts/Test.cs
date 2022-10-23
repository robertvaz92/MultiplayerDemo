using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform[] m_playerList;

    [Range(1, 4)]
    public int m_maxPlayer = 4;

    public float m_distBetweenPlayer = 1.3f;
    [ContextMenu("Place Players")]

    void PlacePlayers()
    {
        for (int i = 0; i < m_playerList.Length; i++)
        {
            m_playerList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < m_maxPlayer; i++)
        {
            m_playerList[i].gameObject.SetActive(true);
            m_playerList[i].position = new Vector3(GetXPosVal(i), -4.5f, -1f);
        }
    }

    float GetXPosVal(int playerIndex)
    {
        return ((((playerIndex * 2) - (m_maxPlayer - 1)) / 2f) * m_distBetweenPlayer);
    }
}
