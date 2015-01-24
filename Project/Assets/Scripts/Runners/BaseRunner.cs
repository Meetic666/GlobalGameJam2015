using UnityEngine;
using System.Collections;

public class BaseRunner : MonoBehaviour 
{
	public enum State
	{
		e_Running,
		e_Tripped,
		e_Carcass,
		e_Eating
	}

	State m_CurrentState;

	public float m_Speed;

	public float m_MaxHorizontalPosition;

	public float m_CollisionDetectionRadius;

	protected Animator m_Animator;
	
	public GameObject m_Normal;
	public GameObject m_Tripped;
	public GameObject m_Carcass;

	public GameObject m_BloodParticles;

	bool m_SetToCarcass = false;

	public State CurrentState
	{
		get
		{
			return m_CurrentState;
		}

		protected set
		{
			m_CurrentState = value;
		}
	}

	public float CurrentSpeed
	{
		get
		{
			return (m_CurrentState == State.e_Running) ? m_Speed : 0.0f;
		}
	}

	// Use this for initialization
	void Start () 
	{
		if(!m_SetToCarcass)
		{
			m_CurrentState = State.e_Running;

			m_Animator = GetComponentInChildren<Animator> ();

			SetToRunning();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(m_CurrentState)
		{
		case State.e_Running:
			DoRunning();			
			UpdateVirtual ();
			CheckForCollisions();
			break;

		case State.e_Tripped:
			DoTripped();
			break;

		case State.e_Carcass:
			DoCarcass();
			break;

		case State.e_Eating:
			UpdateVirtual ();
			break;
		}
	}

	protected virtual void UpdateVirtual()
	{

	}

	void CheckForCollisions()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_CollisionDetectionRadius);

		foreach(Collider otherCollider in colliders)
		{
			BaseRunner otherRunner = otherCollider.GetComponent<BaseRunner>();

			if(otherRunner != null && otherRunner != this)
			{
				HandleCollision(otherRunner);
			}

			EnergyDrink energyDrink = otherCollider.GetComponent<EnergyDrink>();

			if(energyDrink != null)
			{
				HandleCollision(energyDrink);
			}
		}
	}

	protected virtual void HandleCollision(BaseRunner otherRunner)
	{

	}

	protected virtual void HandleCollision(EnergyDrink drink)
	{

	}

	void DoRunning()
	{
		transform.localPosition += transform.forward * m_Speed * Time.deltaTime;
	}

	protected virtual void DoTripped()
	{

	}

	protected virtual void DoCarcass()
	{

	}

	public void SetToCarcass()
	{
		m_SetToCarcass = true;

		m_CurrentState = State.e_Carcass;
		
		if(m_Normal != null)
		{
			m_Normal.SetActive (false);
		}
		
		if(m_Tripped != null)
		{
			m_Tripped.SetActive (false);
		}
		
		if(m_Carcass != null)
		{
			m_Carcass.SetActive (true);
		}
	}

	public void SetToRunning()
	{			
		if(m_Normal != null)
		{
			m_Normal.SetActive (true);
		}
		
		if(m_Tripped != null)
		{
			m_Tripped.SetActive (false);
		}
		
		if (m_Carcass != null) 
		{
			m_Carcass.SetActive (false);
		}
	}

	public void Trip()
	{
		m_CurrentState = State.e_Tripped;

		if(m_Normal != null)
		{
			m_Normal.SetActive (false);
		}
		
		if(m_Tripped != null)
		{
			m_Tripped.SetActive (true);
		}
		
		if(m_Carcass != null)
		{
			m_Carcass.SetActive (false);
		}
	}

	public void Eat()
	{
		SetToCarcass ();

		if(m_BloodParticles != null)
		{
			GameObject particles = (GameObject)Instantiate (m_BloodParticles, transform.position, Quaternion.identity);

			particles.transform.parent = transform;
		}
	}

	public void StartEating()
	{
		m_CurrentState = State.e_Eating;
	}

	public void StopEating()
	{
		m_CurrentState = State.e_Running;
	}
}
