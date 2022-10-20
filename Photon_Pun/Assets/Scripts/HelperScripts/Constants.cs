using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LOBBY_TYPE
{
    eCREATE,
    eJOIN
}


public enum CLICK_ACTION_TYPE
{
    POINTER_DOWN,
    POINTER_UP
}

public enum CURRENT_NAVIGATION_PRESSED
{
    NONE,
    LEFT,
    RIGHT
}

public class Constants 
{
    public const string m_prefsPlayerName = "PLAYER_NAME_PREF";
}
