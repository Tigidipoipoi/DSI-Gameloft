using UnityEngine;
using System.Collections;

public class ConsumableActionsScript : MonoBehaviour {

    private Enemy_Script m_Enemy_Script;
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer== LayerMask.NameToLayer("Enemy"))
        {
           if(ConsumablesManager.instance.m_FreezeEnemy==true)
           {
               m_Enemy_Script = other.gameObject.GetComponent<Enemy_Script>();
               m_Enemy_Script.StartCoroutine("FreezeEnemy");
           }
           if (ConsumablesManager.instance.m_KillAllEnemy == true)
           {
               m_Enemy_Script = other.gameObject.GetComponent<Enemy_Script>();
               m_Enemy_Script.StartCoroutine("FreezeEnemy");
           }
        }
    }


}
