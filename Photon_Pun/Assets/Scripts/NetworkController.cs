using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public static NetworkController m_instance { get; private set; }

    Action m_callback;

    private void Awake()
    {
        m_instance = this;
    }
    ///////////////////////////////////////////////  INITIALIZATION  START /////////////////////////////////////////////////////
    public void ConnectUsingSettings(Action callback)
    {
        PhotonNetwork.ConnectUsingSettings();
        m_callback = callback;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Photon Network : " + PhotonNetwork.CloudRegion + " : Region");
        m_callback?.Invoke();
        m_callback = null;
    }
    ///////////////////////////////////////////////  INITIALIZATION END /////////////////////////////////////////////////////


    ///////////////////////////////////////////////  LOBBY START /////////////////////////////////////////////////////

    public void LobbyOperation(string playerName, LOBBY_TYPE lobbyType, string lobbyName, Action callback)
    {
        PlayerPrefs.SetString(Constants.m_prefsPlayerName, playerName);
        PhotonNetwork.NickName = playerName;
        m_callback = callback;
        switch (lobbyType)
        {
            case LOBBY_TYPE.eCREATE:
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 4;
                PhotonNetwork.CreateRoom(lobbyName, roomOptions);
                break;

            case LOBBY_TYPE.eJOIN:
                PhotonNetwork.JoinRoom(lobbyName);
                break;
        }
    }


    public override void OnJoinedRoom()
    {
        m_callback?.Invoke();
        m_callback = null;
        UpdatePlayersInfo();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        UpdatePlayersInfo();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayersInfo();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayersInfo();
    }

    public void MoveOutOfRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    ///////////////////////////////////////////////  LOBBY END /////////////////////////////////////////////////////


    ///////////////////////////////////////////////  PLAYER INFO START /////////////////////////////////////////////////////
    public Player[] m_players { get; private set; }

    void UpdatePlayersInfo()
    {
        m_players = PhotonNetwork.PlayerList;
        Debug.Log("Total Player Count = " + m_players.Length);
    }


    ///////////////////////////////////////////////  PLAYER INFO END /////////////////////////////////////////////////////


   

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("Is this mine?... " + info.Sender.IsLocal.ToString());
    }
}
