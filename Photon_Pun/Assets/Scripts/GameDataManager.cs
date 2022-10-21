using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager m_instance { get; private set; }

    //Car Textures
    public Texture2D m_blueCarTexture;
    public Texture2D m_greenCarTexture;
    public Texture2D m_yellowCarTexture;
    public Texture2D m_orangeCarTexture;


    //DATA FOR THE RESULTS UI SCREEN 
    public string m_winnerName { get; set; }
    public GAME_RESULTS m_results { get; set; }


    public List<PhotonPlayer> m_photonPlayers;

    private void Awake()
    {
        m_instance = this;
        m_photonPlayers = new List<PhotonPlayer>();
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
