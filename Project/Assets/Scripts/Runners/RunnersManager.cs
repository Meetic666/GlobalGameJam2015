using UnityEngine;
using System.Collections;

public class RunnersManager : MonoBehaviour 
{
	public Player m_Player;

	// Update is called once per frame
	void Update () 
	{
		transform.position -= transform.forward * m_Player.m_Speed * Time.deltaTime;
	}
}
