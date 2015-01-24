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

	protected override void UpdateVirtual ()
	{
		base.UpdateVirtual ();

		if(!m_ResetStartScale)
		{
			m_ResetStartScale = true;

			m_StartScale = transform.localScale;

			m_ScaleSpeed = m_JumpScaleMultiplier / m_JumpTime;
		}

		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxis ("Horizontal") * m_LateralSpeed * Time.deltaTime;

		newPosition.x = Mathf.Clamp (newPosition.x, - m_MaxHorizontalPosition, m_MaxHorizontalPosition);

		transform.position = newPosition;

		if(Input.GetButtonDown("Punch"))
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, m_PunchRadius);

			foreach(Collider otherCollider in colliders)
			{
				People otherPeople = otherCollider.GetComponent<People>();

				if(otherPeople != null && otherPeople != this)
				{
					Punch(otherPeople);

					GameData.Instance.TripPeople();
				}
			}
		}

		Vector3 targetScale = m_StartScale;

		if(m_JumpTimer > 0.0f)
		{
			m_JumpTimer -= Time.deltaTime;

			targetScale = m_StartScale * m_JumpScaleMultiplier;
		}
		else if(Input.GetButtonDown ("Jump"))
		{
			m_JumpTimer = m_JumpTime;
		}

		transform.localScale = Vector3.Lerp (transform.localScale, targetScale, m_ScaleSpeed * Time.deltaTime);


		m_Speed -= m_DecelerationRate * Time.deltaTime;

		if(m_Speed < 0.0f)
		{
			m_Speed = 0.0f;
		}
	}

	protected override void HandleCollision (EnergyDrink drink)
	{
		m_Speed += drink.m_SpeedBoost;

		Destroy (drink.gameObject);
	}
}
