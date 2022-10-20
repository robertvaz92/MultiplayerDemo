using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    public TextMeshProUGUI m_errorText;
    
    float m_displayTimer = 0;
    bool m_canUpdate = false;

    public void Initialize()
    {
        m_errorText.text = "";
        m_displayTimer = 0;
        m_canUpdate = false;
    }

    public void DisplayError(string errorText)
    {
        m_errorText.text = errorText;
        m_displayTimer = 5;
        m_canUpdate = true;
    }

    private void Update()
    {
        if (m_canUpdate)
        {
            m_displayTimer -= Time.deltaTime;
            if (m_displayTimer < 0)
            {
                m_canUpdate = false;
                m_errorText.text = "";
            }
        }
    }
}
