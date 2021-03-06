﻿using UnityEngine;
using System.Collections;

/*
 * Comment émettre un event:
		EventManagerScript.emit(EventManagerType.ENEMY_HIT, this.gameObject);
 * 
 * Comment traiter un event (dans un start ou un initialisation)
		EventManagerScript.onEvent += (EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
 * ou:
		void maCallback(EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
		EventManagerScript.onEvent += maCallback;
 * 
 * qui permet de:
		EventManagerScript.onEvent -= maCallback; //Retire l'appel
 */


public enum EventManagerType
{
	GUN_SHOOT,
	GATLINE_SHOOT,
	SHOT_GUN,
	TOURELLE,
	ITEM_DROP,
	ENEMY_DAMAGE,
	ENEMY_DEATH,
	EXPLOSION,
	TIME_DROP
}

public class EventManagerScript : MonoBehaviour {

	public delegate void EventAction(EventManagerType emt, GameObject go);
	public static event EventAction onEvent;

	#region Singleton
	static private EventManagerScript s_Instance;
	static public EventManagerScript instance
	{
		get
		{
			return s_Instance;
		}
	}
	#endregion

	#region Members
	
	#endregion

	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		//DontDestroyOnLoad(this);
	}

	void Start()
	{
		EventManagerScript.onEvent += (EventManagerType emt, GameObject go) => { Debug.Log("&"); };
	}

	public static void emit(EventManagerType emt, GameObject go)
	{
		
		if(onEvent!=null)
		{
			onEvent(emt,go);
		}



	}


}
