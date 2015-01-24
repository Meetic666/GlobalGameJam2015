using UnityEngine;
using System.Collections;

public class RunnersManager : MonoBehaviour 
{
	public Player m_Player;

	public Renderer m_Background;

	public float m_SpeedTextureOffsetRatio = 1.0f;

	// Update is called once per frame
	void Update () 
	{
		transform.position -= transform.forward * m_Player.CurrentSpeed * Time.deltaTime;

		m_Background.material.mainTextureOffset += new Vector2 (0.0f, m_Player.CurrentSpeed * m_SpeedTextureOffsetRatio * Time.deltaTime);
	}
}
