using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager m_instance { get; private set; }

    [Header("Car Textures")]
    public Texture2D m_blueCarTexture;
    public Texture2D m_greenCarTexture;
    public Texture2D m_yellowCarTexture;
    public Texture2D m_orangeCarTexture;

    // GamePlay Config Data
    public float m_playerMoveSpeed { get; private set; }
    public float m_playerCollisionDistance { get; private set; }

    public float m_roadMoveSpeed { get; private set; }
    public float m_roadEdgeXValue { get; private set; }
    
    public float m_obstacleMoveSpeed { get; private set; }
    public float m_obstacleSpawnGap { get; private set; }

    //Photon Player Local copy with custom data
    public List<PhotonPlayer> m_photonPlayers;

    //DATA FOR THE RESULTS UI SCREEN 
    public string m_winnerName { get; set; }
    public GAME_RESULTS m_results { get; set; }



    private void Awake()
    {
        m_instance = this;
        m_photonPlayers = new List<PhotonPlayer>();

        SetGamePlayValues();
    }

    void SetGamePlayValues()
    {
        m_playerMoveSpeed = 1.5f;
        m_playerCollisionDistance = 0.6f;

        m_roadMoveSpeed = 0.1f;
        m_roadEdgeXValue = 2.5f;
        
        m_obstacleMoveSpeed = 3.15f;
        m_obstacleSpawnGap = 1.25f;
    }

    public void CleanUpData()
    {
        m_photonPlayers.Clear();
    }

    public void CreatePlayerData(Player player, CAR_TYPE cType)
    {
        PhotonPlayer p = new PhotonPlayer();
        p.m_player = player;
        p.m_selectedCarType = cType;
        m_photonPlayers.Add(p);
    }

    public Texture2D GetCarTexture(CAR_TYPE cType)
    {
        Texture2D retVal = m_blueCarTexture;
        switch (cType)
        {
            case CAR_TYPE.GREEN:
                retVal = m_greenCarTexture;
                break;
            case CAR_TYPE.YELLOW:
                retVal = m_yellowCarTexture;
                break;
            case CAR_TYPE.ORANGE:
                retVal = m_orangeCarTexture;
                break;
        }
        return retVal;
    }
}
