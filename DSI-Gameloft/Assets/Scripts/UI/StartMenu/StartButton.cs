using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    #region Members
    public GameObject m_PanelOption;
    public Transform m_InitialPosition;

    public bool m_Open;

    int m_LoadProgress = 0;
    #endregion

    public void LaunchGame() {
        Application.LoadLevel("PlayTestV2");
    }

    IEnumerator DisplayLoadingScreen() {
        // SetActive Loading Screen

        AsyncOperation async = Application.LoadLevelAsync("PlayTestV2");
        while (!async.isDone) {
            m_LoadProgress = (int)(async.progress * 100.0f);
            yield return null;
        }
    }

    public void Exit() {
        Application.Quit();
    }
}
