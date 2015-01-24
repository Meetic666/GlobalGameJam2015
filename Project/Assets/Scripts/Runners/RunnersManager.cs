using UnityEngine;
using System.Collections;

public class RunnersManager : MonoBehaviour 
{
	public Player[] m_Players;

	public Renderer m_Background;

	public float m_SpeedTextureOffsetRatio = 1.0f;

	// Update is called once per frame
	void Update () 
	{
		float currentSpeed = 0.0f;

		foreach(Player player in m_Players)
		{
			if(player.CurrentSpeed > currentSpeed)
			{
				currentSpeed = player.CurrentSpeed;
			}
		}

		transform.position -= transform.forward * currentSpeed * Time.deltaTime;

		m_Background.material.mainTextureOffset += new Vector2 (0.0f, currentSpeed * m_SpeedTextureOffsetRatio * Time.deltaTime);
	}
}
