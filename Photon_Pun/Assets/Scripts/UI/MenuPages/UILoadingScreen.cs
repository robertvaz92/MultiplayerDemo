using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : UIPageBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        m_networkController.ConnectUsingSettings(OnConnectedToMaster);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void OnConnectedToMaster()
    {
        MenuHandler.GetInstance().RequestState(eMenuStates.LOBBY);
    }
}
