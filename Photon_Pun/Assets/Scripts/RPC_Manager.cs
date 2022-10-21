using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Realtime;

public class RPC_Manager : MonoBehaviour
{
    public static RPC_Manager m_instance { get; private set; }

    PhotonView m_view;
    public Action<Player, CAR_TYPE> m_carSelectCallback;

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


    //////////////////////////////// CAR SELECT RPC /////////////////////////////



    //////////////////////////////// CAR SELECT RPC /////////////////////////////
}
