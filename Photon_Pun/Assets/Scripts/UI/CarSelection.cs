using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public CAR_TYPE m_carType;
    public Button m_carButton;
    public TextMeshProUGUI m_playerNameText;

    UIPlayerListScreen m_screen;
    public Player m_photonPlayer { get; private set; }
    public void Initialize(UIPlayerListScreen screen)
    {
        m_screen = screen;
        m_playerNameText.text = "";
        RemovePlayer();
    }

    public void OnClickCar()
    {
        m_screen.OnCarSelectedRPC(m_carType);
    }

    public void SetPlayer(Player player)
    {
        m_photonPlayer = player;
        m_playerNameText.text = m_photonPlayer.NickName;

        m_carButton.interactable = false;
    }

    public void RemovePlayer()
    {
        m_photonPlayer = null;
        m_playerNameText.text = "";

        m_carButton.interactable = true;
    }

    public void SavePlayerData()
    {
        if (m_photonPlayer != null)
        {
            GameDataManager.m_instance.CreatePlayerData(m_photonPlayer, m_carType);
        }
    }
}
