using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManagerScript.onEvent += FXOne;
	}

	void OnDestroy()
	{
		EventManagerScript.onEvent -= FXOne;
	}


	#region Members
	private Transform m_Player;
	private Transform m_PlayerChild;
	private GameObject Instance;

	public GameObject FXGunShoot;
	public GameObject FXGatLine;
	public GameObject FXShotGun;

	public GameObject FXTourelle;
	#endregion


	void FXOne(EventManagerType emt, GameObject go)
	{
		m_Player = go.transform;
		while (m_Player.parent != null)
		{
			m_PlayerChild = m_Player;
			m_Player = m_Player.parent;
		}

		switch(emt)
		{
			//Bullets
			case EventManagerType.GUN_SHOOT:
				Instance = Instantiate(FXGunShoot, go.transform.position, m_PlayerChild.rotation) as GameObject;
				Instance.transform.Rotate(m_PlayerChild.up, 180.0f);
				Destroy(Instance, 1.0f);
				break;

			case EventManagerType.GATLINE_SHOOT:
				Instance = Instantiate(FXGatLine, go.transform.position, m_PlayerChild.rotation) as GameObject;
				Instance.transform.Rotate(m_PlayerChild.up, 180.0f);
				Destroy(Instance, 1.0f);
				break;

			case EventManagerType.SHOT_GUN:
				Instance = Instantiate(FXShotGun, go.transform.position, m_PlayerChild.rotation) as GameObject;
				Instance.transform.Rotate(m_PlayerChild.up, 180.0f);
				Destroy(Instance, 1.0f);
				break;

			case EventManagerType.TOURELLE:
				Instance = Instantiate(FXTourelle, go.transform.position, m_PlayerChild.rotation) as GameObject;
				Instance.transform.Rotate(m_PlayerChild.up, 180.0f);
				Destroy(Instance, 0.35f);
				break;

		}

	}

}
