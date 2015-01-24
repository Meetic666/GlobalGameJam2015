using UnityEngine;
using System.Collections;

public class Player : People 
{
	public float m_DecelerationRate;
	public float m_LateralSpeed;

	public float m_JumpTime;
	float m_JumpTimer;

	public float m_JumpScaleMultiplier = 1.2f;

	Vector3 m_StartScale;
	bool m_ResetStartScale;
	float m_ScaleSpeed;

	public float m_TrippedTime;
	float m_TrippedTimer;

	public int m_PlayerNumber;

	ParticleSystem m_TempParticleHolder;

	protected override void UpdateVirtual ()
	{
		base.UpdateVirtual ();

		if(!m_ResetStartScale)
		{
			m_ResetStartScale = true;

			m_StartScale = transform.localScale;

			m_ScaleSpeed = m_JumpScaleMultiplier / m_JumpTime;

			if(!GameData.Instance.PlayersConfirmed[m_PlayerNumber - 1])
			{
				gameObject.SetActive(false);
			}
			else if(GameData.Instance.NumberOfPlayersConfirmed == 1)
			{
				m_TrippedTime = 600.0f;
			}
		}

		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxis ("Horizontal" + m_PlayerNumber) * m_LateralSpeed * Time.deltaTime;

		newPosition.x = Mathf.Clamp (newPosition.x, - m_MaxHorizontalPosition, m_MaxHorizontalPosition);

		transform.position = newPosition;

		if(Input.GetButtonDown("Punch" + m_PlayerNumber))
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, m_PunchRadius);

			foreach(Collider otherCollider in colliders)
			{
				People otherPeople = otherCollider.GetComponent<People>();

				if(otherPeople != null && otherPeople != this)
				{
					Punch(otherPeople);

					GameData.Instance.TripPeople(m_PlayerNumber);
				}
			}
		}

		Vector3 targetScale = m_StartScale;

		if(m_JumpTimer > 0.0f)
		{
			m_JumpTimer -= Time.deltaTime;

			if(m_JumpTimer > m_JumpTime * 0.5f)
			{
				targetScale = m_StartScale * m_JumpScaleMultiplier;
			}
		}
		else
		{
			m_IsJumping = false;

			if(Input.GetButtonDown ("Jump" + m_PlayerNumber))
			{
				m_JumpTimer = m_JumpTime;

				m_IsJumping = true;
			}
		}

		transform.localScale = Vector3.Lerp (transform.localScale, targetScale, m_ScaleSpeed * Time.deltaTime);


		m_Speed -= m_DecelerationRate * Time.deltaTime;

		if(m_Speed < 0.0f)
		{
			m_Speed = 0.0f;
		}

		GameData.Instance.UpdateScore (Time.deltaTime, m_PlayerNumber);

		bool canBoost = false;

		if(m_LionPack != null)
		{
			foreach(Lion lion in m_LionPack.Lions)
			{
				if(transform.position.z < lion.transform.position.z + m_LimitZThreshold)
				{
					canBoost =true;
				}
			}
		}

		if(canBoost && Input.GetButtonDown ("Boost"+m_PlayerNumber))
		{
			m_Speed += m_LimitZSpeedBoost;
		}
	}

	protected override void HandleCollision (EnergyDrink drink)
	{
		if(drink.gameObject.activeSelf)
		{
			m_Speed += drink.m_SpeedBoost;
		m_TempParticleHolder = ParticleHelper.Instance.FunEnergy (transform.position);
		m_TempParticleHolder.transform.parent = transform;

			drink.gameObject.SetActive(false);
		}
	}

	protected override void DoCarcass ()
	{
		base.DoCarcass ();

		transform.localScale = m_StartScale;
	}

	protected override void DoTripped ()
	{
		if(m_TrippedTimer > 0.0f)
		{
			m_TrippedTimer -= Time.deltaTime;

			if(m_TrippedTimer <= 0.0f)
			{
				CurrentState = State.e_Running;

				SetToRunning ();
			}
		}
		else
		{
			m_TrippedTimer = m_TrippedTime;
		}
	}
}
