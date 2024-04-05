using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlayScreen : UIPageBase
{
    public GamePlayManager m_gamePlayManager;
    public ButtonEvent m_leftButton;
    public ButtonEvent m_rightButton;

    CURRENT_NAVIGATION_PRESSED m_navigationType;

    public override void OnStart(MenuStates ID)
    {
        base.OnStart(ID);
        m_gamePlayManager.Initialize(this);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        m_navigationType = CURRENT_NAVIGATION_PRESSED.NONE;
        m_leftButton.Initialize(OnClickNavigationLeft);
        m_rightButton.Initialize(OnClickNavigationRight);
        m_gamePlayManager.StartGame();
    }

    public override void OnExit()
    {
        base.OnExit();
        m_gamePlayManager.StopGame();
    }

    public override void OnUpdate()
    {
        UpdateButtonClick();
        m_gamePlayManager.CustomUpdate();
    }


    void UpdateButtonClick()
    {
        switch (m_navigationType)
        {
            case CURRENT_NAVIGATION_PRESSED.LEFT:
                m_gamePlayManager.OnLeftButtonPress?.Invoke();
                break;

            case CURRENT_NAVIGATION_PRESSED.RIGHT:
                m_gamePlayManager.OnRighttButtonPress?.Invoke();
                break;
        }
    }

    public void OnClickNavigationRight(CLICK_ACTION_TYPE clickType)
    {
        m_navigationType = CURRENT_NAVIGATION_PRESSED.NONE;
        if (clickType == CLICK_ACTION_TYPE.POINTER_DOWN)
        {
            m_navigationType = CURRENT_NAVIGATION_PRESSED.RIGHT;
        }
    }
    public void OnClickNavigationLeft(CLICK_ACTION_TYPE clickType)
    {
        m_navigationType = CURRENT_NAVIGATION_PRESSED.NONE;
        if (clickType == CLICK_ACTION_TYPE.POINTER_DOWN)
        {
            m_navigationType = CURRENT_NAVIGATION_PRESSED.LEFT;
        }
    }

    public void GameOver()
    {
        MenuHandler.GetInstance().RequestState(MenuStates.RESULTS);
    }
}
