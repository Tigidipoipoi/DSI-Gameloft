using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    #region Members
    public GameObject m_PanelOption;
    public Transform m_InitialPosition;

    public bool m_Open;
    public GameObject m_LoadingScreenTrans;
    #endregion

    public void LaunchGame() {
        this.StartCoroutine("LaunchGameCoroutine");
    }

    IEnumerator LaunchGameCoroutine() {
        m_LoadingScreenTrans.SetActive(true);

        yield return null;
        Application.LoadLevel("PlayTestV2");
    }

    public void Exit() {
        Application.Quit();
    }
}
