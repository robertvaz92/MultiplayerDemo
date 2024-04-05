using UnityEngine;
using System.Collections;
using static UIPageBase;

public class MenuController : MonoBehaviour
{
    [Header("UI Screns")]
    public UIPageBase m_loadingScreen;
    public UIPageBase m_lobbyScreen;
    public UIPageBase m_playerListScreen;
    public UIPageBase m_gameplayScreen;
    public UIPageBase m_resultsScreen;

    void Awake()
	{
        Application.targetFrameRate = 60;
		MenuHandler.GetInstance ().OnAwake(this);
	}
	void Start ()
	{
        m_loadingScreen.OnStart(MenuStates.LOADING);
        m_lobbyScreen.OnStart(MenuStates.LOBBY);
        m_playerListScreen.OnStart(MenuStates.PLAYER_LIST);
        m_gameplayScreen.OnStart(MenuStates.GAMEPLAY);
        m_resultsScreen.OnStart(MenuStates.RESULTS);

        SetFirstPage();
	}

	void SetFirstPage()
	{
		MenuHandler.GetInstance().RequestState(MenuStates.LOADING);
	}
	
	void Update ()
	{
		MenuHandler.GetInstance().SelectState();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuHandler.GetInstance().OnEscapeButtonPress();
        }
	}
}
