using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerListScreen : UIPageBase
{
    public TextMeshProUGUI m_nameText;
    WaitForSeconds m_oneSec;
    string m_playerNames;
    Player[] m_players;
    public override void OnEnter()
    {
        base.OnEnter();
        m_oneSec = new WaitForSeconds(1);
        StartCoroutine(DisplayPlayerList());
    }

    IEnumerator DisplayPlayerList()
    {
        while (true)
        {
            m_playerNames = "";
            m_players = PhotonNetwork.PlayerList;
            for (int i = 0; i < m_players.Length; i++)
            {
                m_playerNames += m_players[i].NickName + "  : " + m_players[i].ActorNumber;
                m_playerNames += "\n";
            }
            m_nameText.text = m_playerNames;
            yield return m_oneSec;

            if (m_players.Length > 1)
            {
                MenuHandler.GetInstance().RequestState(eMenuStates.GAMEPLAY);
            }
        }
    }
}
