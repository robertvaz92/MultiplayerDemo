using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LOBBY_TYPE
{
    CREATE,
    JOIN,
    LEAVE
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

public enum NETWORK_RESPONSE_STATUS
{
    SUCCESS,
    FAILED
}

public class NetworkResponse
{
    public NETWORK_RESPONSE_STATUS m_status;
    public string m_message;
    public short m_code;

    public NetworkResponse(NETWORK_RESPONSE_STATUS status, string message, short code)
    {
        m_status = status;
        m_message = message;
        m_code = code;
    }
}

public class Constants 
{
    public const string m_prefsPlayerName = "PLAYER_NAME_PREF";
}
