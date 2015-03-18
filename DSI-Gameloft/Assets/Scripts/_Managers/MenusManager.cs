using UnityEngine;
using System.Collections;

public class MenusManager : MonoBehaviour {

	#region Singleton
	static private MenusManager s_Instance;
	static public MenusManager instance
	{
		get
		{
			return s_Instance;
		}
	}

	#region members
	public Animator m_Animator;
	#endregion

	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
	}
	#endregion

	public void ChangeAnimation(string change)
	{
		m_Animator.ResetTrigger(change);
		m_Animator.SetTrigger(change);
	}

}
