using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreen : UIPageBase
{
    public Image m_carIcon;
    public Sprite[] m_carSprites;
    public AnimationCurve m_carMoveCurve;
    public float m_speed = 1;
    public AudioSource m_audioSource;

    float m_lerpStep;
    int m_carIndex;

    Vector3 m_pos;
    bool m_isCircleComplete;
    bool m_isConnectedToMaster;
    public override void OnEnter()
    {
        base.OnEnter();
        m_networkController.ConnectUsingSettings(OnConnectedToMaster);
        m_isCircleComplete = false;
        m_isConnectedToMaster = false;
        m_pos = new Vector3(-300, -330, 0);
        m_carIcon.sprite = m_carSprites[0];
        m_carIcon.transform.localPosition = m_pos;
        m_carIndex = -1;
        m_lerpStep = 1;
    }

    public void OnConnectedToMaster(NetworkResponse response)
    {
        m_isConnectedToMaster = true;
    }

    public override void OnUpdate()
    {
        m_lerpStep += Time.deltaTime * m_speed;
        if (m_lerpStep > 1)
        {
            m_lerpStep = 0;
            m_carIndex++;
            if (m_carIndex >= m_carSprites.Length)
            {
                m_carIndex = 0;
                m_isCircleComplete = true;
            }
            else
            {
                m_audioSource.Stop();
                m_audioSource.Play();
            }

            m_carIcon.sprite = m_carSprites[m_carIndex];
        }

        m_pos.x = Mathf.Lerp(-300, 300, m_carMoveCurve.Evaluate(m_lerpStep));
        m_carIcon.transform.localPosition = m_pos;

        if (m_isCircleComplete && m_isConnectedToMaster)
        {
            MenuHandler.GetInstance().RequestState(MenuStates.LOBBY);
            m_isCircleComplete = false;
            m_isConnectedToMaster = false;
        }
    }
}
