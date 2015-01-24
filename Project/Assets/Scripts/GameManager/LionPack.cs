using UnityEngine;
using System.Collections;

public class LionPack : MonoBehaviour 
{
	Lion[] m_Lions;

	// Use this for initialization
	void Start () 
	{
		m_Lions = GetComponentsInChildren<Lion> ();

		foreach(Lion lion in m_Lions)
		{
			lion.LionPack = this;
		}
	}
	
	public void StopPackForEating(BaseRunner meal)
	{
		foreach(Lion lion in m_Lions)
		{
			lion.StartEating();

			lion.SetMeal (meal);
		}
	}

	public void ResumeRunning()
	{
		foreach(Lion lion in m_Lions)
		{
			lion.StopEating();
		}
	}
}
