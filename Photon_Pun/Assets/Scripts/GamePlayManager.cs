using Photon.Pun;
using Photon.Realtime;
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

    public Action OnLeftButtonPress;
    public Action OnRighttButtonPress;

    public CarPlayer m_carPlayerPrefab;
    List<CarPlayer> m_playerList;
    List<CarPlayer> m_currentPlayerList;
    UIGamePlayScreen m_gameplayScreen;


    

    public void Initialize(UIGamePlayScreen screen)
    {
        m_gameplayScreen = screen;
        m_road.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        m_road.Initialize(this);
        m_obsManager.Initialize(this);
        m_road.gameObject.SetActive(true);
        RPC_Manager.m_instance.m_playerMoveCallback += MovePlayerCallback;
        RPC_Manager.m_instance.m_playerDieCallback += DiePlayerCallback;

        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        List<PhotonPlayer> photonPlayers = GameDataManager.m_instance.m_photonPlayers;
        m_playerList = new List<CarPlayer>();
        m_currentPlayerList = new List<CarPlayer>();
        for (int i = 0; i < photonPlayers.Count; i++)
        {
            CarPlayer p = Instantiate<CarPlayer>(m_carPlayerPrefab, new Vector3(GetXPosVal(i+1, photonPlayers.Count), -4.5f, 0), Quaternion.identity);
            m_playerList.Add(p);
            m_currentPlayerList.Add(p);
            p.Initialize(this, photonPlayers[i]);
        }

        //Assign Side by side Players
        for (int i = 0; i < m_playerList.Count; i++)
        {
            if (i > 0)
            {
                m_playerList[i].m_leftCar = m_playerList[i - 1];
            }
            if (i < m_playerList.Count-1)
            {
                m_playerList[i].m_rightCar = m_playerList[i + 1];
            }
        }
    }

    float GetXPosVal(int playerNumber, int maxPlayer)
    {
        float retVal = 1;

        if (playerNumber == 1)
        {
            retVal = -1;
        }

        return retVal;
    }

    void CheckForGameOver()
    {
        if (m_currentPlayerList.Count == 1)
        {
            GameDataManager.m_instance.m_winnerName = m_currentPlayerList[0].m_photonPlayer.m_player.NickName;

            if (m_currentPlayerList[0].m_photonPlayer.m_player.IsLocal)
            {
                GameDataManager.m_instance.m_results = GAME_RESULTS.WIN;
            }
            else
            {
                GameDataManager.m_instance.m_results = GAME_RESULTS.LOSE;
            }

            m_gameplayScreen.GameOver();
        }
    }

    public void StopGame()
    {
        RPC_Manager.m_instance.m_playerMoveCallback -= MovePlayerCallback;
        RPC_Manager.m_instance.m_playerDieCallback -= DiePlayerCallback;
        m_road.gameObject.SetActive(false);
        m_obsManager.BeforeDestroy();

        for (int i = 0; i < m_playerList.Count; i++)
        {
            m_playerList[i].BeforeDestroy();
           Destroy(m_playerList[i].gameObject);
        }
        m_playerList.Clear();
    }

    public void CustomUpdate()
    {
        m_road.CustomUpdate();
        m_obsManager.CustomUpdate();
    }


    //// RPC FUNCTIONS CALLBACKS

    void MovePlayerCallback(Player sender, float xPos)
    {
        for (int i = 0; i < m_playerList.Count; i++)
        {
            if (sender == m_playerList[i].m_photonPlayer.m_player)
            {
                m_playerList[i].MovePlayer(xPos);
                break;
            }
        }
    }

    void DiePlayerCallback(Player sender)
    {
        for (int i = 0; i < m_playerList.Count; i++)
        {
            if (sender == m_playerList[i].m_photonPlayer.m_player)
            {
                m_playerList[i].DiePlayer();
                m_currentPlayerList.Remove(m_playerList[i]);
                break;
            }
        }
        CheckForGameOver();
    }
}
