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

	public State CurrentState
	{
		get
		{
			return m_CurrentState;
		}
	}

	// Use this for initialization
	void Start () 
	{
		m_CurrentState = State.e_Running;
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

			if(otherRunner != null)
			{
				HandleCollision(otherRunner);
			}
		}
	}

	protected virtual void HandleCollision(BaseRunner otherRunner)
	{

	}

	void DoRunning()
	{
		transform.localPosition += transform.forward * m_Speed * Time.deltaTime;
	}

	void DoTripped()
	{

	}

	void DoCarcass()
	{

	}

	public void Trip()
	{
		m_CurrentState = State.e_Tripped;
	}

	public void Eat()
	{
		m_CurrentState = State.e_Carcass;
	}

	protected void StartEating()
	{
		m_CurrentState = State.e_Eating;
	}

	protected void StopEating()
	{
		m_CurrentState = State.e_Running;
	}
}
