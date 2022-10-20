using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILobbyScreen : UIPageBase
{
    public ErrorMessage m_errorMsgText;
    public TMP_InputField m_playerNameInput;

    public TMP_InputField m_roomNameInput;


    public override void OnEnter()
    {
        base.OnEnter();
        m_playerNameInput.text = PlayerPrefs.GetString(Constants.m_prefsPlayerName, "");
        m_errorMsgText.Initialize();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void OnClickCreate()
    {
        if (ValidateInputField())
        {
            m_networkController.LobbyOperation(m_playerNameInput.text, LOBBY_TYPE.eCREATE, m_roomNameInput.text, OnJoinedRoom);
        }
    }

    public void OnClickJoin()
    {
        if (ValidateInputField())
        {
            m_networkController.LobbyOperation(m_playerNameInput.text, LOBBY_TYPE.eJOIN, m_roomNameInput.text, OnJoinedRoom);
        }
    }

    public void OnJoinedRoom()
    {
        MenuHandler.GetInstance().RequestState(eMenuStates.PLAYER_LIST);
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
