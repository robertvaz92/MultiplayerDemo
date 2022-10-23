using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Photon.Realtime;

public enum CAR_TYPE
{
    BLUE,
    GREEN,
    YELLOW,
    ORANGE
}

public enum GAME_RESULTS
{
    WIN,
    LOSE
}

public enum PLAYER_STATE
{
    LOBBY,
    GAME_PLAY
}


public class PhotonPlayer
{
    public Player m_player;
    public CAR_TYPE m_selectedCarType;
    public PLAYER_STATE m_currentState;
    public float m_xPos;
}

public class Utilities
{
    
}


