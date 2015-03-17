using UnityEngine;
using System.Collections;

public enum EventManagerType
{
	PLAYER_HIT,
	ENEMY_HIT,
	SHOT_BULLET
}

public class EventManagerScript : MonoBehaviour {
	#region Singleton
	static private EventManagerScript s_Instance;
	static public EventManagerScript instance
	{
		get
		{
			return s_Instance;
		}
	}



	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		//DontDestroyOnLoad(this);
	}
	#endregion

	public void emit(EventType et, GameObject go)
	{


	}

}
