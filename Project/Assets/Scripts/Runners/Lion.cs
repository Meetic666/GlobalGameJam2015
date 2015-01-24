using UnityEngine;
using System.Collections;

public class Lion : BaseRunner 
{
	public float m_EatDelay;
	float m_EatTimer;

	LionPack m_LionPack;

	public LionPack LionPack
	{
		set
		{
			m_LionPack = value;
		}
	}

	protected override void UpdateVirtual ()
	{
		if(m_EatTimer > 0.0f)
		{
			m_EatTimer -= Time.deltaTime;

			if(m_EatTimer <= 0.0f)
			{
				StopEating();

				m_LionPack.ResumeRunning();
			}
		}
	}

	protected override void HandleCollision (BaseRunner otherRunner)
	{
		if(!(otherRunner is Lion) && !(otherRunner is Ex) && (otherRunner.CurrentState == State.e_Running
		   || otherRunner.CurrentState == State.e_Tripped))
		{
			StartEating ();
			otherRunner.Eat ();

			m_LionPack.StopPackForEating();

			m_EatTimer = m_EatDelay;
		}
	}
}
