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
			case EventManagerType.GUN_SHOOT:
				Instance = Instantiate(FXGunShoot, go.transform.position, m_PlayerChild.rotation) as GameObject;
				Instance.transform.eulerAngles = new Vector3(Instance.transform.rotation.x, m_PlayerChild.transform.rotation.y , Instance.transform.rotation.z);
				break;

			case EventManagerType.GATLINE_SHOOT:
				Instantiate(FXGatLine, go.transform.position, go.transform.rotation);
				break;

			case EventManagerType.SHOT_GUN:
				Instantiate(FXShotGun, go.transform.position, go.transform.rotation);
				break;

		}

	}

}
