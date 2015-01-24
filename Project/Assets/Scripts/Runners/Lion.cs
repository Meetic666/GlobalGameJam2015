using UnityEngine;
using System.Collections;

public class Lion : BaseRunner 
{
	public float m_EatDelay;
	float m_EatTimer;

	LionPack m_LionPack;

	float m_StartX;
	float m_StartZ;
	bool m_ResetStartX;

	float m_MealX;

	public float m_LateralSpeed;

	public LionPack LionPack
	{
		set
		{
			m_LionPack = value;
		}
	}

	protected override void UpdateVirtual ()
	{
		if(!m_ResetStartX)
		{
			m_ResetStartX = true;

			m_StartX = transform.localPosition.x;
			m_StartZ = transform.position.z;
		}

		float targetX = m_StartX;

		if(m_EatTimer > 0.0f)
		{
			m_EatTimer -= Time.deltaTime;

			targetX = m_MealX;

			if(m_EatTimer <= 0.0f)
			{
				StopEating();

				m_LionPack.ResumeRunning();
			}
		}
		
		Vector3 newPosition = transform.localPosition;
		
		newPosition.x = Mathf.Lerp(newPosition.x, targetX, m_LateralSpeed * Time.deltaTime);
		
		transform.localPosition = newPosition;

		
		if(transform.position.z < m_StartZ)
		{
			newPosition = transform.position;

			newPosition.z = m_StartZ;
			
			transform.position = newPosition;
		}
	}

	protected override void HandleCollision (BaseRunner otherRunner)
	{
		if(!(otherRunner is Lion) && !(otherRunner is Ex) && (otherRunner.CurrentState == State.e_Running
		   || otherRunner.CurrentState == State.e_Tripped))
		{
			StartEating ();
			otherRunner.Eat ();

			m_LionPack.StopPackForEating(otherRunner);

			m_EatTimer = m_EatDelay;
		}
	}

	public void SetMeal(BaseRunner meal)
	{
		m_MealX = meal.transform.localPosition.x;

		m_EatTimer = m_EatDelay;
	}
}
