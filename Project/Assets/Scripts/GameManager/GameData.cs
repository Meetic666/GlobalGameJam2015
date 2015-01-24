using UnityEngine;
using System.Collections;

public class GameData
{
	private static GameData s_Instance;

	public static GameData Instance
	{
		get
		{
			if(s_Instance == null)
			{
				s_Instance = new GameData();
			}

			return s_Instance;
		}
	}

	float m_DickPercentage;

	public float DickPercentage 
	{
		get
		{
			return m_DickPercentage;
		}

		set
		{
			m_DickPercentage = value;

			Mathf.Clamp01(m_DickPercentage);
		}
	}
	
	
	float m_DickBoostForTrippingPeople = 0.1f;

	private GameData()
	{

	}

	public void TripPeople()
	{
		m_DickPercentage += m_DickBoostForTrippingPeople;
	}
}
