using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class UIPlayerListScreen : UIPageBase
{
    public TextMeshProUGUI m_nameText;
    public GameObject m_selectCarPanel;
    public GameObject m_waitingPlayersToJoinPanel;
    public CarSelection[] m_carSelection;
    public GameObject m_exitConfirmation;
    public GameObject m_startGameButton;
    string m_playerNames;
    Player[] m_players;
    List<Player> m_playersList;
    int m_playerCount;
    bool m_allPlayersJoined;
    public override void OnEnter()
    {
        base.OnEnter();
        m_exitConfirmation.SetActive(false);
        m_startGameButton.SetActive(false);
        m_playerCount = 0;
        m_allPlayersJoined = false;
        m_playersList = new List<Player>();

        DisplayPanel(m_selectCarPanel, true);
        DisplayPanel(m_waitingPlayersToJoinPanel, false);

        for (int i = 0; i < m_carSelection.Length; i++)
        {
            m_carSelection[i].Initialize(this);
        }
        RPC_Manager.m_instance.m_carSelectCallback += OnCarClick;
    }

    public override void OnExit()
    {
        RPC_Manager.m_instance.m_carSelectCallback -= OnCarClick;

        for (int i = 0; i < m_carSelection.Length; i++)
        {
            m_carSelection[i].RemovePlayer();
        }

        base.OnExit();
    }

    void UpdatePlayerList()
    {
        m_playerNames = "";
        m_playerCount = 0;
        m_allPlayersJoined = false;

        m_players = NetworkController.m_instance.m_players;
        if (m_players != null)
        {
            for (int i = 0; i < m_players.Length; i++)
            {
                m_playerNames += m_players[i].NickName + "  : " + m_players[i].ActorNumber;
                m_playerNames += "\n";
            }
            m_nameText.text = m_playerNames;
            m_playerCount = m_players.Length;
        }

        if (m_playerCount >= 1)
        {
            m_allPlayersJoined = true;
        }
    }

    void DisplayPanel(GameObject panel, bool shouldDisplay)
    {
        if (panel.activeSelf != shouldDisplay)
        {
            panel.SetActive(shouldDisplay);
        }
    }

    //Change this implementation to Trigger based
    public override void OnUpdate()
    {
        UpdatePlayerList();

        if (m_allPlayersJoined)
        {
            DisplayPanel(m_selectCarPanel, true);
            DisplayPanel(m_waitingPlayersToJoinPanel, false);
        }
        else
        {
            DisplayPanel(m_selectCarPanel, false);
            DisplayPanel(m_waitingPlayersToJoinPanel, true);
            for (int i = 0; i < m_carSelection.Length; i++)
            {
                m_carSelection[i].RemovePlayer();
            }
            m_playersList.Clear();
        }
    }

    public void OnCarSelectedRPC(CAR_TYPE cType)
    {
        RPC_Manager.m_instance.OnCarSelectedRPC(cType);
    }


    void OnCarClick(Player sender, CAR_TYPE cType)
    {
        for (int i = 0; i < m_carSelection.Length; i++)
        {
            if (m_carSelection[i].m_carType == cType)
            {
                m_carSelection[i].SetPlayer(sender);

                for (int j = 0; j < m_carSelection.Length; j++)
                {
                    if (i != j)
                    {
                        if (m_carSelection[j].m_photonPlayer != null)
                        {
                            if (m_carSelection[j].m_photonPlayer == sender)
                            {
                                m_carSelection[j].RemovePlayer();
                            }
                        }
                    }
                }
            }
        }

        if (!m_playersList.Contains(sender))
        {
            m_playersList.Add(sender);
        }
        CheckAllPlayersSelectedCar();
    }

    void CheckAllPlayersSelectedCar()
    {
        if (m_playersList.Count >= m_players.Length)
        {
            for (int i = 0; i < m_carSelection.Length; i++)
            {
                m_carSelection[i].SavePlayerData();
            }

            if (NetworkController.m_instance.m_localPlayer.IsMasterClient)
            {
                m_startGameButton.SetActive(true);
            }
        }
    }


    public void OnClickStartGame()
    {
        MenuHandler.GetInstance().RequestState(MenuStates.GAMEPLAY);
    }

    public void OnClickCloseButton()
    {
        m_exitConfirmation.SetActive(true);
    }

    public void OnClickExitConfirm()
    {
        m_networkController.LobbyOperation(PlayerPrefs.GetString(Constants.m_prefsPlayerName), 0, LOBBY_TYPE.LEAVE, "", null);
        MenuHandler.GetInstance().RequestState(MenuStates.LOBBY);
    }

    public void OnClickExitCancel()
    {
        m_exitConfirmation.SetActive(false);
    }
}