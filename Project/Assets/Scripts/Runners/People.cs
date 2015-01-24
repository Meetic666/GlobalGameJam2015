using UnityEngine;
using System.Collections;

public class People : BaseRunner 
{	
	public float m_PunchDelay;
	float m_PunchTimer;

	public float m_PunchRadius;

	public People m_Player;

	protected bool m_IsJumping;

	public float m_PunchLength = 0.5f;
	float m_PunchLengthTimer;

	int m_PunchDirection;

	public float m_LimitZ;
	public float m_LimitZSpeedBoost = 20.0f;

	protected override void UpdateVirtual ()
	{
		if(m_Player != null && Vector3.Distance (m_Player.transform.position, transform.position) < m_PunchRadius)
		{			
			if(m_PunchTimer > 0.0f)
			{
				m_PunchTimer -= Time.deltaTime;
				
				if(m_PunchTimer <= 0.0f)
				{
					Punch (m_Player);
				}
			}
			else
			{				
				m_PunchTimer = m_PunchDelay * (1.0f - GameData.Instance.DickPercentage);
			}
		}

		if(m_PunchLengthTimer > 0.0f)
		{
			if(m_Animator != null)
			{
				m_Animator.SetBool("Punching", true);
				
				m_Animator.SetInteger ("PunchDirection", m_PunchDirection);
			}

			m_PunchLengthTimer -= Time.deltaTime;
		}
		else
		{
			if(m_Animator != null)
			{
				m_Animator.SetBool("Punching", false);
			}
		}

		if(transform.position.z < m_LimitZ)
		{
			m_Speed += m_LimitZSpeedBoost;
		}
	}

	protected void Punch(People otherPeople)
	{
		otherPeople.Trip ();

		m_PunchLengthTimer = m_PunchLength;

		m_PunchDirection = (int)Mathf.Sign (otherPeople.transform.position.x - transform.position.x);
	}

	protected override void HandleCollision (BaseRunner otherRunner)
	{
		if(!m_IsJumping && otherRunner.CurrentState == BaseRunner.State.e_Carcass)
		{
			Trip();
		}
	}
}
