using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResultsScreen : UIPageBase
{
    public TextMeshProUGUI m_titleText;
    public TextMeshProUGUI m_infoText;

    public override void OnEnter()
    {
        base.OnEnter();
        switch (GameDataManager.m_instance.m_results)
        {
            case GAME_RESULTS.WIN:
                m_titleText.text = "You Won!";
                m_infoText.text = string.Format("Congratulation! \n {0} \n On Your Victory", GameDataManager.m_instance.m_winnerName);
                break;
            case GAME_RESULTS.LOSE:
                m_titleText.text = "You Lost!";
                m_infoText.text = string.Format("{0} has won the game \n Better luck next time", GameDataManager.m_instance.m_winnerName);
                break;
        }
    }

    public void OnCLickContinue()
    {
        MenuHandler.GetInstance().RequestState(eMenuStates.PLAYER_LIST);
    }
}
