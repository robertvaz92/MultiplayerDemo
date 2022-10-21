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


public class PhotonPlayer
{
    public Player m_player;
    public CAR_TYPE m_selectedCarType;
}

public class Utilities
{
    
}


