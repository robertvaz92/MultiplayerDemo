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
        m_loadingScreen.OnStart(eMenuStates.LOADING);
        m_lobbyScreen.OnStart(eMenuStates.LOBBY);
        m_playerListScreen.OnStart(eMenuStates.PLAYER_LIST);
        m_gameplayScreen.OnStart(eMenuStates.GAMEPLAY);
        m_resultsScreen.OnStart(eMenuStates.RESULTS);

        SetFirstPage();
	}

	void SetFirstPage()
	{
		MenuHandler.GetInstance().RequestState(eMenuStates.LOADING);
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
