using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    public GamePlayManager m_manager;
    public CarPlayer m_leftCar;
    public CarPlayer m_rightCar;

    PhotonView m_view;

    Vector3 m_pos;
    Vector3 m_opponentPos;
    bool m_canMove;
    public void Initialize(GamePlayManager manager, PhotonView view)
    {
        m_manager = manager;
        if (view.IsMine)
        {
            m_view = view;
            //m_view.RPC("InitRPC", RpcTarget.All);
            StartCoroutine(DelayedCall());
            m_manager.OnLeftButtonPress += OnPressLeft;
            m_manager.OnRighttButtonPress += OnPressRight;
        }
    }

    IEnumerator DelayedCall()
    {
        yield return new WaitForSeconds(2);
        m_view.RPC("InitRPC", RpcTarget.All);
    }

    [PunRPC]
    void InitRPC()
    {
        gameObject.name = PhotonNetwork.LocalPlayer.NickName;
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
            transform.position = m_pos;
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
            transform.position = m_pos;
        }
    }
}
