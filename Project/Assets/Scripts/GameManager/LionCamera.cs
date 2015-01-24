using UnityEngine;
using System.Collections;

public class LionCamera : MonoBehaviour 
{
	public float m_ShakeTime = 0.5f;
	public float m_ShakeAmplitude = 10.0f;
	public float m_ShakeSpeed = 1.0f;

	float m_ShakeTimer;
	float m_CenterX;

	bool m_IsShaking;

	void Start()
	{
		m_CenterX = transform.position.x;
	}

	void Update()
	{
		Vector3 newPosition = transform.position;

		if(m_IsShaking)
		{
			m_ShakeTimer += Time.deltaTime;

			newPosition.x = m_CenterX + m_ShakeAmplitude * Mathf.Cos (m_ShakeSpeed * m_ShakeTimer);

			if(m_ShakeTimer > m_ShakeTime)
			{
				m_IsShaking = false;
			}
		}
		else
		{
			newPosition.x = Mathf.Lerp (newPosition.x, m_CenterX, m_ShakeSpeed * Time.deltaTime);
		}

		transform.position = newPosition;
	}

	public void StartShaking()
	{
		m_IsShaking = true;

		m_ShakeTimer = 0.0f;
	}
}
