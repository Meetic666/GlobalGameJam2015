using UnityEngine;
using System.Collections;

public class Player : People 
{
	public float m_DecelerationRate;
	public float m_LateralSpeed;

	public float m_JumpTime;
	float m_JumpTimer;

	protected override void UpdateVirtual ()
	{
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
				}
			}
		}

		if(m_JumpTimer > 0.0f)
		{
			m_JumpTimer -= Time.deltaTime;
		}
		else if(Input.GetButtonDown ("Jump"))
		{
			m_JumpTimer = m_JumpTime;
		}

		m_Speed -= m_DecelerationRate * Time.deltaTime;
	}
}
