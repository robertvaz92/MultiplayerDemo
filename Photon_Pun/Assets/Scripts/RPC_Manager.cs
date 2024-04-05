using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Realtime;

/// <summary>
/// To Broadcast the function call to all the players, including the sender
/// </summary>
public class RPC_Manager : MonoBehaviour//, IPunObservable
{
    public static RPC_Manager m_instance { get; private set; }

    PhotonView m_view;
    public Action<Player, CAR_TYPE> m_carSelectCallback;
    public Action<Player, float> m_playerMoveCallback;
    public Action<Player> m_playerLeaveLobbyCallback;
    public Action<Player> m_playerDieCallback;
    public Action<float> m_obstacleSpawnCallback;

    private void Awake()
    {
        m_view = GetComponent<PhotonView>();
        m_instance = this;
    }


    //////////////////////////////// CAR SELECT RPC /////////////////////////////
    public void OnCarSelectedRPC(CAR_TYPE cType)
    {
        m_view.RPC("OnCarSelect", RpcTarget.All, cType);
    }


    [PunRPC]
    void OnCarSelect(CAR_TYPE cType, PhotonMessageInfo info)
    {
        m_carSelectCallback?.Invoke(info.Sender, cType);
    }


    //////////////////////////////// MOVE PLAYER RPC /////////////////////////////

    public void MovePlayerRPC(float xVal)
    {
        m_view.RPC("OnMovePlayer", RpcTarget.All, xVal);
    }

    [PunRPC]
    void OnMovePlayer(float xPos, PhotonMessageInfo info)
    {
        m_playerMoveCallback?.Invoke(info.Sender, xPos);
    }

    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(0);
        }
        else if(stream.IsReading)
        {
            m_playerMoveCallback?.Invoke(info.Sender, (float)stream.ReceiveNext());
        }
    }
    */

    //////////////////////////////// DIE PLAYER RPC /////////////////////////////

    public void DiePlayerRPC()
    {
        m_view.RPC("OnDiePlayer", RpcTarget.All);
    }

    [PunRPC]
    void OnDiePlayer(PhotonMessageInfo info)
    {
        m_playerDieCallback?.Invoke(info.Sender);
    }

    //////////////////////////////// OBSTACLE SPAWN RPC /////////////////////////////
    public void SpawnObstacle(float xPos)
    {
        m_view.RPC("OnSpawnObs", RpcTarget.All, xPos);
    }

    [PunRPC]
    void OnSpawnObs(float xPos)
    {
        m_obstacleSpawnCallback?.Invoke(xPos);
    }

   

    //////////////////////////////// NEXT RPC /////////////////////////////
}
