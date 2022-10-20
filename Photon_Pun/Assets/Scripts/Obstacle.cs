using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Obstacle : MonoBehaviour
{
    ObstacleManager m_manager;
    Transform m_objTrans;
    Vector3 m_pos;
    bool m_isActivated = false;
    PhotonView m_view;

    public void Initialize(ObstacleManager manager)
    {
        m_manager = manager;
        m_objTrans = transform;
        m_view = GetComponent<PhotonView>();
        m_view.RPC("DisableObject", RpcTarget.All);
    }

    public void Activate()
    {
        m_isActivated = true;
        m_view.RPC("EnableObject", RpcTarget.All);
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
                m_view.RPC("DisableObject", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void EnableObject()
    {
        gameObject.SetActive(true);
    }
    [PunRPC]
    void DisableObject()
    {
        gameObject.SetActive(false);
    }

    

}