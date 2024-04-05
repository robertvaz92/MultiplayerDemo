using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuHandler
{
    public MenuController m_menuController { get; private set; }

    private static MenuHandler m_instance;
    public static MenuHandler GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = new MenuHandler();
        }

        return m_instance;
    }

    public List<UIPageBase> m_pages;

    public UIPageBase m_currentPage { get; private set; }
    private UIPageBase m_requestedPage;
    private UIPageBase m_lastPage;
    private bool m_switchPage = false;

    public void OnAwake(MenuController controller)
    {
        m_pages = new List<UIPageBase>();

        m_currentPage = null;
        m_requestedPage = null;
        m_lastPage = null;

        m_menuController = controller;
    }


    public void RegisterPage(UIPageBase page)
    {
        if (!IsStateExist(page.PageID))
        {
            m_pages.Add(page);
        }
    }

    public bool IsStateExist(MenuStates state)
    {
        for (int i = 0; i < m_pages.Count; i++)
        {
            if (m_pages[i].PageID == state)
            {
                return true;
            }
        }

        return false;
    }

    public void RequestState(MenuStates state)
    {
        m_requestedPage = GetState(state);

        m_switchPage = false;
        if (m_requestedPage != null && m_currentPage == null)
        {
            m_switchPage = true;
        }
        if (m_requestedPage != null && m_currentPage != null && m_requestedPage.PageID != m_currentPage.PageID)
        {
            m_switchPage = true;
        }


        if (m_switchPage)
        {
            m_lastPage = m_currentPage;

            if (m_currentPage != null)
            {
                m_currentPage.OnExit();
                m_currentPage = GetState(m_requestedPage.PageID);
                m_currentPage.OnEnter();
            }
            else
            {
                m_currentPage = GetState(m_requestedPage.PageID);
                m_currentPage.OnEnter();
            }
        }
    }

    UIPageBase GetState(MenuStates state)
    {
        for (int i = 0; i < m_pages.Count; i++)
        {
            if (m_pages[i].PageID == state)
            {
                return m_pages[i];
            }
        }

        return null;
    }

    public void SelectState()
    {
        if (m_currentPage != null)
        {
            m_currentPage.OnUpdate();
        }
    }


    public void OnEscapeButtonPress()
    {
        if (m_currentPage != null)
        {
            m_currentPage.OnEscapeButtonPress();
        }
    }
}