using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    #region Members
    public GameObject m_PanelOption;
    public Transform m_InitialPosition;

    public bool m_Open;
    #endregion

    public void LaunchGame () {
        Application.LoadLevel ("PlayTestV2");
    }

    public void Exit () {
        Application.Quit ();
    }
}
