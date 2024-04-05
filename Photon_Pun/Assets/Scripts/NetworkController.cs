using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public static NetworkController m_instance { get; private set; }
    public Player m_localPlayer { get { return PhotonNetwork.LocalPlayer; } }
    Action<NetworkResponse> m_callback;

    private void Awake()
    {
        m_instance = this;
    }
    ///////////////////////////////////////////////  INITIALIZATION  START /////////////////////////////////////////////////////
    public void ConnectUsingSettings(Action<NetworkResponse> callback)
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
        NetworkResponse response = new NetworkResponse(NETWORK_RESPONSE_STATUS.SUCCESS, $"Photon Network : { PhotonNetwork.CloudRegion } : Region", 0);
        m_callback?.Invoke(response);
        m_callback = null;
    }

    ///////////////////////////////////////////////  INITIALIZATION END /////////////////////////////////////////////////////


    ///////////////////////////////////////////////  LOBBY START /////////////////////////////////////////////////////

    public void LobbyOperation(string playerName, int maxPlayers, LOBBY_TYPE lobbyType, string lobbyName, Action<NetworkResponse> callback)
    {
        PlayerPrefs.SetString(Constants.m_prefsPlayerName, playerName);
        PhotonNetwork.NickName = playerName;
        m_callback = callback;
        switch (lobbyType)
        {
            case LOBBY_TYPE.CREATE:
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = (byte)maxPlayers;
                PhotonNetwork.CreateRoom(lobbyName, roomOptions);
                break;

            case LOBBY_TYPE.JOIN:
                PhotonNetwork.JoinRoom(lobbyName);
                break;

            case LOBBY_TYPE.LEAVE:
                PhotonNetwork.LeaveRoom();
                break;
        }
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        NetworkResponse response = new NetworkResponse(NETWORK_RESPONSE_STATUS.FAILED, message, returnCode);
        m_callback?.Invoke(response);
        m_callback = null;
        UpdatePlayersInfo();
    }

    public override void OnJoinedRoom()
    {
        NetworkResponse response = new NetworkResponse(NETWORK_RESPONSE_STATUS.SUCCESS, "Joined Room", 0);
        m_callback?.Invoke(response);
        m_callback = null;
        UpdatePlayersInfo();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        NetworkResponse response = new NetworkResponse(NETWORK_RESPONSE_STATUS.FAILED, message, returnCode);
        m_callback?.Invoke(response);
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

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        //PhotonNetwork.CurrentRoom.CustomProperties;
    }
}
