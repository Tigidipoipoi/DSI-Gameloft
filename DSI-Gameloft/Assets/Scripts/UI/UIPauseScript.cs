using UnityEngine;
using System.Collections;

public class UIPauseScript : MonoBehaviour {

    private bool m_PauseOn;

    public void LaunchPause()
    {
        if(m_PauseOn==false)
        {
            m_PauseOn = true;
            Time.timeScale = 0.0F;
        }
        else
        {
            m_PauseOn = false;
            Time.timeScale = 1.0F;
        }
    }

}
