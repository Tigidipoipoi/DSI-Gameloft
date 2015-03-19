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

	public GameObject FXTimeDrop;

	public GameObject FXTourelle;

	public GameObject FXEnemyDamage;

	public GameObject FXEnemyDeath;

	public GameObject FXExplosion;
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

			case EventManagerType.ENEMY_DAMAGE:
				Instance = Instantiate(FXEnemyDamage, go.transform.position, go.transform.rotation) as GameObject;
				Destroy(Instance, 1.7f);
				break;

			case EventManagerType.ENEMY_DEATH:
				Instance = Instantiate(FXEnemyDeath, go.transform.position, go.transform.rotation) as GameObject;
				break;

			case EventManagerType.EXPLOSION:
				Instance = Instantiate(FXExplosion, go.transform.position, go.transform.rotation) as GameObject;
				Destroy(Instance, 1.0f);
				break;


		}

	}

}
