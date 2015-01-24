using UnityEngine;
using System.Collections;

public class Lion : BaseRunner 
{
	public float m_EatDelay;
	float m_EatTimer;

	protected override void UpdateVirtual ()
	{
		if(m_EatTimer > 0.0f)
		{
			m_EatTimer -= Time.deltaTime;

			if(m_EatTimer <= 0.0f)
			{
				StopEating();
			}
		}
	}

	protected override void HandleCollision (BaseRunner otherRunner)
	{
		if(otherRunner.CurrentState == State.e_Running
		   || otherRunner.CurrentState == State.e_Tripped)
		{
			StartEating ();
			otherRunner.Eat ();
		}
	}
}
