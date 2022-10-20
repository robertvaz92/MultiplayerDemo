using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePlayManager : MonoBehaviour
{
    public float m_speed = 0.25f;
    public float m_obsSpeed = 2f;
    public Road m_road;
    public ObstacleManager m_obsManager;
    public GameObject m_playerPrefab;
    public GameObject m_playerObject;

    public Action OnLeftButtonPress;
    public Action OnRighttButtonPress;

    CarPlayer m_carPlayer;

    public void Initialize()
    {
        m_road.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        m_road.Initialize(this);
        m_obsManager.Initialize(this);

        m_road.gameObject.SetActive(true);
        m_playerObject = PhotonNetwork.Instantiate(m_playerPrefab.name, new Vector3(0,-4.5f,0), Quaternion.identity);
        PhotonView view = m_playerObject.GetComponent<PhotonView>();
        m_carPlayer = m_playerObject.GetComponent<CarPlayer>();
        m_carPlayer.Initialize(this, view);
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("I was just spawned!" + gameObject.name);
    }


    public void StopGame()
    {
        m_road.gameObject.SetActive(false);
        m_carPlayer.BeforeDestroy();
        m_obsManager.BeforeDestroy();
       //Call destroy player object;
    }

    public void CustomUpdate()
    {
        m_road.CustomUpdate();
        m_obsManager.CustomUpdate();
    }
}
