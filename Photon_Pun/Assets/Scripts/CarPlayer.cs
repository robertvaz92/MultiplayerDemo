using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    public Renderer m_playerMeshRenderer;

    public GamePlayManager m_manager;
    public CarPlayer m_leftCar;
    public CarPlayer m_rightCar;

    public PhotonPlayer m_photonPlayer;
    public bool m_isDead { get; private set; }
    

    Vector3 m_pos;
    Vector3 m_movePos;
    Vector3 m_opponentPos;
    bool m_canMove;
    LayerMask m_obsLayer;

    public void Initialize(GamePlayManager manager, PhotonPlayer photonPlayer)
    {
        m_manager = manager;
        m_photonPlayer = photonPlayer;
        m_obsLayer = LayerMask.NameToLayer("Obstacle");
        m_playerMeshRenderer.material.mainTexture = GameDataManager.m_instance.GetCarTexture(photonPlayer.m_selectedCarType);
        m_isDead = false;
        if (m_photonPlayer.m_player.IsLocal)
        {
            m_manager.OnLeftButtonPress += OnPressLeft;
            m_manager.OnRighttButtonPress += OnPressRight;
        }
    }

    public void BeforeDestroy()
    {
        m_manager.OnLeftButtonPress -= OnPressLeft;
        m_manager.OnRighttButtonPress -= OnPressRight;
    }

    public void OnPressLeft()
    {
        m_canMove = false;
        m_pos = transform.position;
        m_pos.x -= Time.deltaTime * 1;

        if (m_leftCar != null)
        {
            m_opponentPos = m_leftCar.transform.position;
            if ((m_pos.x - m_opponentPos.x) > 0.7f)
            {
                m_canMove = true;
            }
        }
        else if (m_pos.x > -2.5f)
        {
            m_canMove = true;
        }

        if (m_canMove)
        {
            m_manager.MovePlayerRPC(m_pos.x);
        }
    }

    public void OnPressRight()
    {
        m_canMove = false;
        m_pos = transform.position;
        m_pos.x += Time.deltaTime * 1;
        if (m_rightCar != null)
        {
            m_opponentPos = m_rightCar.transform.position;
            if ((m_opponentPos.x - m_pos.x) > 0.7f)
            {
                m_canMove = true;
            }
        }
        else if (m_pos.x < 2.5f)
        {
            m_canMove = true;
        }
        if (m_canMove)
        {
            m_manager.MovePlayerRPC(m_pos.x);
        }
    }

    public void MovePlayer(float x)
    {
        m_movePos = transform.position;
        m_movePos.x = x;
        transform.position = m_movePos;
    }

    public void DiePlayer()
    {
        if (m_leftCar != null)
        {
            m_leftCar.m_rightCar = null;
        }

        if (m_rightCar != null)
        {
            m_rightCar.m_leftCar = null;
        }

        m_playerMeshRenderer.material.color = Color.red;
        BeforeDestroy();
        m_isDead = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_photonPlayer.m_player.IsLocal)
        {
            if (other.gameObject.layer == m_obsLayer)
            {
                m_manager.DiePlayerRPC();
            }
        }
    }
}
