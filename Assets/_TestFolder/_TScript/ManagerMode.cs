using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMode {

    private bool m_IsActive = false;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void ToggleState()
    {
        if (!m_IsActive)
        {
            m_IsActive = true;
        }
        else
        {
            m_IsActive = false;
        }
    }
    public bool GetIsActive()
    { 
        return  m_IsActive;
    }

}
