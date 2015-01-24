using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class People : BaseRunner 
{	
	public float m_PunchDelay;
	float m_PunchTimer;

	public float m_PunchRadius;

	public List<Player> m_Players;

	protected bool m_IsJumping;

	public float m_PunchLength = 0.5f;
	float m_PunchLengthTimer;

	int m_PunchDirection;

	public float m_LimitZThreshold;
	public float m_LimitZSpeedBoost = 20.0f;
	public LionPack m_LionPack;

	protected override void UpdateVirtual ()
	{
		foreach(Player player in m_Players)
		{
			if(player.gameObject.activeSelf && Vector3.Distance (player.transform.position, transform.position) < m_PunchRadius)
			{			
				if(m_PunchTimer > 0.0f)
				{
					m_PunchTimer -= Time.deltaTime;
					
					if(m_PunchTimer <= 0.0f)
					{
						Punch (player);
					}
				}
				else
				{				
					m_PunchTimer = m_PunchDelay * (1.0f - GameData.Instance.DickPercentages[player.m_PlayerNumber - 1]);

					if(m_PunchTimer <= 0.0f)
					{
						Punch (player);
					}

				}
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

		if(m_LionPack != null)
		{
			foreach(Lion lion in m_LionPack.Lions)
			{
				if(transform.position.z < lion.transform.position.z + m_LimitZThreshold)
				{
					m_Speed += m_LimitZSpeedBoost;
				}
			}
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
