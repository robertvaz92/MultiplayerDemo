using UnityEngine;
using System;

public enum eMenuStates
{
	LOADING,
	LOBBY,
	PLAYER_LIST,
	GAMEPLAY,
	RESULTS
}


public class UIPageBase : MonoBehaviour
{
    public eMenuStates PageID { get; private set; }
	protected NetworkController m_networkController { get; private set; }

	public virtual void OnEnter()
	{
		gameObject.SetActive(true);
		m_networkController = NetworkController.m_instance;
	}
	

	public virtual void OnExit()
	{
		gameObject.SetActive(false);
	}


	public virtual void OnStart(eMenuStates ID)
	{
        PageID = ID;
        MenuHandler.GetInstance().RegisterPage(this);
        gameObject.SetActive(false);
    }

	public virtual void OnEscapeButtonPress()
	{
		
	}
    
	public virtual void OnUpdate()
	{

	}
}
