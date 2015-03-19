using UnityEngine;
using System.Collections;

public class ActivateAnimators : MonoBehaviour {
    #region Members
    public Animator m_UIAchievementsAnimator;
    public Animator m_UICreditsAnimator;
    public Animator m_UIEndLevelAnimator;
    public Animator m_UILeaderboardAnimator;
    public Animator m_UILevelSelectAnimator;
    public Animator m_UPlayscreenAnimator;
    public Animator m_UISettingsAnimator;
    public Animator m_UIShopAnimator;
    #endregion

    void Start() {
        this.StartCoroutine("ActivateAnimator");
    }

    IEnumerator ActivateAnimator() {
        yield return null;


    }
}
