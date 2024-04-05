using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILobbyScreen : UIPageBase
{
    public ErrorMessage m_errorMsgText;
    public TMP_InputField m_playerNameInput;

    public TMP_InputField m_roomNameInput;
    public TextMeshProUGUI m_maxPlayerCount;

    int m_maxPlayers;
    public override void OnEnter()
    {
        base.OnEnter();
        m_playerNameInput.text = PlayerPrefs.GetString(Constants.m_prefsPlayerName, "");
        m_errorMsgText.Initialize();
        m_maxPlayers = 2;
        m_maxPlayerCount.text = m_maxPlayers.ToString();
    }

    public void OnClickRight()
    {
        if (m_maxPlayers < 4)
        {
            m_maxPlayers++;
            m_maxPlayerCount.text = m_maxPlayers.ToString();
        }
    }

    public void OnClickLeft()
    {
        if (m_maxPlayers > 1)
        {
            m_maxPlayers--;
            m_maxPlayerCount.text = m_maxPlayers.ToString();
        }
    }

    public void OnClickCreate()
    {
        if (ValidateInputField())
        {
            m_networkController.LobbyOperation(m_playerNameInput.text, m_maxPlayers, LOBBY_TYPE.CREATE, m_roomNameInput.text, OnJoinedRoom);
        }
    }

    public void OnClickJoin()
    {
        if (ValidateInputField())
        {
            m_networkController.LobbyOperation(m_playerNameInput.text, m_maxPlayers, LOBBY_TYPE.JOIN, m_roomNameInput.text, OnJoinedRoom);
        }
    }

    public void OnJoinedRoom(NetworkResponse response)
    {
        if (response.m_status == NETWORK_RESPONSE_STATUS.SUCCESS)
        {
            MenuHandler.GetInstance().RequestState(MenuStates.PLAYER_LIST);
        }
        else
        {
            m_errorMsgText.DisplayError(response.m_message);    
        }
    }

    bool ValidateInputField()
    {
        bool retVal = false;
        if (string.IsNullOrEmpty(m_playerNameInput.text))
        {
            m_errorMsgText.DisplayError("Enter Your Name");
        }
        else 
        {
            if (string.IsNullOrEmpty(m_roomNameInput.text))
            {
                m_errorMsgText.DisplayError("Enter Room Name");
            }
            else 
            {
                retVal = true;
            }
        }
        return retVal;
    }
}
