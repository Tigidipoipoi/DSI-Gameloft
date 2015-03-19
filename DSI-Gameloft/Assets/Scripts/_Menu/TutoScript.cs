using UnityEngine;
using System.Collections;

public class TutoScript : MonoBehaviour 
{

	public int m_Valeur;

    public Animator Anim1;
    public Animator Anim2;
    public Animator Anim3;
    public Animator Anim4;
    public Animator Anim5;
    public Animator Anim6;
    public Animator Anim7;
	public Animator Anim8;
	public Animator Anim9;
	public Animator Anim10;
	public Animator Anim11;
	public Animator Anim12;
	public Animator Anim13;
	public Animator Anim14;
	public Animator Anim15;
	public Animator Anim16;
	public Animator Anim17;
	public Animator Anim18;
	
	void Start()
	{
		Anim1.SetInteger("valeur", 1);
	}

	public void LaunchAnim(int valeur)
	{
		//M_Valeur panneau actuel 
		//valeur ancien panneau

		Debug.Log("m_Valeur "+m_Valeur);
		Debug.Log("valeur "+valeur);


		//ON efface l'ancien
		switch (m_Valeur)
		{
			case 0:
				Application.LoadLevel("StartMenu");
				break;

			case 1:
				Anim1.SetInteger("valeur", 0);
				break;
			case 2:
				Anim2.SetInteger("valeur", 0);
				break;
			case 3:
				Anim3.SetInteger("valeur", 0);
				break;
			case 4:
				Anim4.SetInteger("valeur", 0);
				break;
			case 5:
				Anim5.SetInteger("valeur", 0);
				break;
			case 6:
				Anim6.SetInteger("valeur", 0);
				break;
			case 7:
				Anim7.SetInteger("valeur", 0);
				break;
			case 8:
				Anim8.SetInteger("valeur", 0);
				break;
			case 9:
				Anim9.SetInteger("valeur", 0);
				break;
			case 10:
				Anim10.SetInteger("valeur", 0);
				break;
			case 11:
				Anim11.SetInteger("valeur", 0);
				break;
			case 12:
				Anim12.SetInteger("valeur", 0);
				break;
			case 13:
				Anim13.SetInteger("valeur", 0);
				break;
			case 14:
				Anim14.SetInteger("valeur", 0);
				break;
			case 15:
				Anim15.SetInteger("valeur", 0);
				break;
			case 16:
				Anim16.SetInteger("valeur", 0);
				break;
			case 17:
				Anim17.SetInteger("valeur", 0);
				break;
			case 18:
				Anim18.SetInteger("valeur", 0);
				break;
			case 19:
				Application.LoadLevel("StartMenu");
				break;
		}



		m_Valeur += valeur;
	
		
		//ON met le nouveau panneau
		switch (m_Valeur)
		{
			case 0:
				Application.LoadLevel("StartMenu");
				break;
			case 1:
				Anim1.SetInteger("valeur", 1);
				break;
			case 2:
				Anim2.SetInteger("valeur", 1);
				break;
			case 3:
				Anim3.SetInteger("valeur", 1);
				break;
			case 4:
				Anim4.SetInteger("valeur", 1);
				break;
			case 5:
				Anim5.SetInteger("valeur", 1);
				break;
			case 6:
				Anim6.SetInteger("valeur", 1);
				break;
			case 7:
				Anim7.SetInteger("valeur", 1);
				break;
			case 8:
				Anim8.SetInteger("valeur", 1);
				break;
			case 9:
				Anim9.SetInteger("valeur", 1);
				break;
			case 10:
				Anim10.SetInteger("valeur", 1);
				break;
			case 11:
				Anim11.SetInteger("valeur", 1);
				break;
			case 12:
				Anim12.SetInteger("valeur", 1);
				break;
			case 13:
				Anim13.SetInteger("valeur", 1);
				break;
			case 14:
				Anim14.SetInteger("valeur", 1);
				break;
			case 15:
				Anim15.SetInteger("valeur", 1);
				break;
			case 16:
				Anim16.SetInteger("valeur", 1);
				break;
			case 17:
				Anim17.SetInteger("valeur", 1);
				break;
			case 18:
				Anim18.SetInteger("valeur", 1);
				break;
			case 19:
				Application.LoadLevel("StartMenu");
				break;
		}
        
    }

}
