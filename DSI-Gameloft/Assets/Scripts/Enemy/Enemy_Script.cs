using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {
    #region Members
    // Playtest Only !
    public int m_DropTheKey = -1;

    float c_EnemyYPosClamp;

    public float m_Life;

    public bool m_IsAwake;

    public Transform m_Player;

    public Renderer m_Renderer;
    public Material m_Material;

    public GameObject m_EnnemyMissile2;

    public float m_EarnedTime;
    public GameObject m_TimeDistributor;
    private TimeDistributor m_TimeDistributorScript;
    private GameObject m_KeyPrefab;

    float m_FreezeDelay;
    protected bool m_IsFreeze;
    #endregion

    public void Awake() {
        m_KeyPrefab = Resources.Load<GameObject>("Prefabs/Key/Key");
    }

    public virtual void Start() {

        c_EnemyYPosClamp = this.transform.position.y;

        //m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        FloorManager.instance.NewEnemyAppeared();

        m_FreezeDelay = 6.5f;

        if (m_Renderer == null) {
            m_Renderer = this.GetComponent<Renderer>();
        }
        m_Material = m_Renderer.material;
    }

    public void GetDamage(float m_Damage) {
		EventManagerScript.emit(EventManagerType.ENEMY_DAMAGE, this.gameObject);
        StartCoroutine(blink());
        m_Life -= m_Damage;
        if (m_Life <= 0) {
            this.DestroyEnemy();
        }
    }

    public IEnumerator FreezeEnemy() {
        m_IsFreeze = true;
        yield return new WaitForSeconds(m_FreezeDelay);
        m_IsFreeze = false;
    }

    public void DestroyEnemy() {
		EventManagerScript.emit(EventManagerType.ENEMY_DEATH, this.gameObject);
        bool mustPopKey = m_DropTheKey > -1
            ? m_DropTheKey > 0
            : FloorManager.instance.MustPopKey();

        if (name == "EnemyMissile" && m_EnnemyMissile2 != null) {
            GameObject firstChild = Instantiate(m_EnnemyMissile2, this.transform.position, this.transform.rotation) as GameObject;
            GameObject secondChild = Instantiate(m_EnnemyMissile2, this.transform.position, this.transform.rotation) as GameObject;

            firstChild.GetComponent<Enemy_Script>().m_DropTheKey = m_DropTheKey > 0
                ? 0 : m_DropTheKey;
            secondChild.GetComponent<Enemy_Script>().m_DropTheKey = m_DropTheKey > 0
                ? 0 : m_DropTheKey;
        }

        m_Player.GetComponent<PlayerScript>().Unlock();

		if (TimerManager.instance.m_RemainingTime >= 0)
		{
			GameObject m_PrefabTimeDistributor = Instantiate(m_TimeDistributor, this.transform.position, this.transform.rotation) as GameObject;
			m_TimeDistributorScript = m_PrefabTimeDistributor.GetComponent<TimeDistributor>();
			m_TimeDistributorScript.m_EarnTime = m_EarnedTime;
		}
        if (mustPopKey) {
            PopKey();
        }

        Destroy(this.gameObject);
    }

    public virtual void Update() {
        if (m_Renderer.IsVisibleFrom(Camera.main)) {
            m_IsAwake = true;
            m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else {
            m_IsAwake = false;
        }

        if (m_Player != null) {
            Vector3 playerPos = m_Player.transform.position;
            playerPos.y = c_EnemyYPosClamp;
            transform.LookAt(playerPos, Vector3.up);
        }
    }

    public IEnumerator blink(float time = 0.5f) {
        float delay = 0.15f;

        while (time > 0) {
			if (m_Material.GetFloat("_Dommages") == 0.0f)
			{
				m_Material.SetFloat("_Dommages", 1.0f);
            }
            else {
				m_Material.SetFloat("_Dommages", 0.0f);
            }

            yield return new WaitForSeconds(delay);

            time -= delay;
        }

		m_Material.SetFloat("_Dommages", 0.0f);
        yield return null;
    }

    void PopKey() {
        Object.Instantiate(m_KeyPrefab, this.transform.position, Quaternion.identity);
    }
}
