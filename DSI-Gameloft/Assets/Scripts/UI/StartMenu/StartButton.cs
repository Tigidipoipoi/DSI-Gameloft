using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    #region Members
    public GameObject m_PanelOption;
    public Transform m_InitialPosition;

    public bool m_Open;
    public GameObject m_LoadingScreenTrans;
    #endregion

    // Put the UIstuff things
    public Animator AnimAchievements; // done
    public Animator AnimCredits; // done
    public Animator AnimFinLevel; // pas dans le menu principal en fait.
    public Animator AnimLeaderboard; //done
    public Animator AnimLevelSelect; // done
    public Animator AnimPlayScreen;
    public Animator AnimSettings;
    public Animator AnimShop; // done




    public void LaunchGame() {
        this.StartCoroutine("LaunchGameCoroutine");
    }

    public void LaunchGame2() {
        this.StartCoroutine("LaunchGameCoroutine");
    }

    IEnumerator LaunchGameCoroutine() {
        m_LoadingScreenTrans.SetActive(true);

        yield return null;
        Application.LoadLevel("PlayTestV2");
    }

    IEnumerator LaunchGame2Coroutine() {
        m_LoadingScreenTrans.SetActive(true);

        yield return null;
        Application.LoadLevel("PlayTestV3");
    }

    public void Tutoriel() {
        Application.LoadLevel("Tutoriel");
    }

    public void Exit() {
        Application.Quit();
    }

    public void Achievements() {
        AnimAchievements.SetInteger("valeur", 1);
    }
    public void Credits() {
        AnimCredits.SetInteger("valeur", 1);
    }

    public void Leaderboard() {
        AnimLeaderboard.SetInteger("valeur", 1);
    }

    public void LevelSelect() {
        AnimLevelSelect.SetInteger("valeur", 1);
    }

    public void PlayScreen() {
        AnimPlayScreen.SetInteger("valeur", 1);
    }

    public void Settings() {
        AnimSettings.SetInteger("valeur", 1);
    }

    public void Shop() {
        AnimShop.SetInteger("valeur", 1);
    }


}
