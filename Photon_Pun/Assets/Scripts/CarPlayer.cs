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

    public void Initialize(GamePlayManager manager, PhotonPlayer photonPlayer, float xPos)
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
        m_photonPlayer.m_xPos = xPos;
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
        m_pos.x -= Time.deltaTime * GameDataManager.m_instance.m_playerMoveSpeed;

        if (m_leftCar != null)
        {
            m_opponentPos = m_leftCar.transform.position;
            if ((m_pos.x - m_opponentPos.x) > GameDataManager.m_instance.m_playerCollisionDistance)
            {
                m_canMove = true;
            }
        }
        else if (m_pos.x > -GameDataManager.m_instance.m_roadEdgeXValue)
        {
            m_canMove = true;
        }

        if (m_canMove)
        {
            RPC_Manager.m_instance.MovePlayerRPC(m_pos.x);
        }
    }

    public void OnPressRight()
    {
        m_canMove = false;
        m_pos = transform.position;
        m_pos.x += Time.deltaTime * GameDataManager.m_instance.m_playerMoveSpeed;
        if (m_rightCar != null)
        {
            m_opponentPos = m_rightCar.transform.position;
            if ((m_opponentPos.x - m_pos.x) > GameDataManager.m_instance.m_playerCollisionDistance)
            {
                m_canMove = true;
            }
        }
        else if (m_pos.x < GameDataManager.m_instance.m_roadEdgeXValue)
        {
            m_canMove = true;
        }
        if (m_canMove)
        {
            RPC_Manager.m_instance.MovePlayerRPC(m_pos.x);
        }
    }

    public void MovePlayer(float x)
    {
        m_movePos = transform.position;
        m_photonPlayer.m_xPos = x;
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
                RPC_Manager.m_instance.DiePlayerRPC();
            }
        }
    }
    /*
    private void Update()
    {
        m_movePos = transform.position;
        if (false && m_photonPlayer.m_player.IsLocal)
        {
            m_movePos.x = m_photonPlayer.m_xPos;
        }
        else
        {
            m_movePos.x = Mathf.Lerp(m_movePos.x, m_photonPlayer.m_xPos, 0.5f);
        }
        transform.position = m_movePos;
    }
*/
}
