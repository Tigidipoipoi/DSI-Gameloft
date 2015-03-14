using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public GameObject m_PanelOption;
    public Transform m_InitialPosition;

    public bool m_Open;

   

    public void LaunchGame()
    {
        Application.LoadLevel("TestLab");
    }

   

    public void Exit()
    {
        Application.Quit();
    }
}
