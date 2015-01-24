using UnityEngine;
using System.Collections;

public class People : BaseRunner 
{	
	public float m_PunchDelay;
	float m_PunchTimer;

	public float m_PunchRadius;

	public People m_Player;

	protected bool m_IsJumping;

	protected override void UpdateVirtual ()
	{
		if(Vector3.Distance (m_Player.transform.position, transform.position) < m_PunchRadius)
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
				m_PunchTimer = m_PunchDelay;
			}
		}
	}

	protected void Punch(People otherPeople)
	{
		otherPeople.Trip ();
	}

	protected override void HandleCollision (BaseRunner otherRunner)
	{
		if(!m_IsJumping && otherRunner.CurrentState == BaseRunner.State.e_Carcass)
		{
			Trip();
		}
	}
}
