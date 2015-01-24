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
	float m_DickDecayRate = 0.01f;

	float m_Score;

	public float Score
	{
		get
		{
			return m_Score;
		}
	}

	int m_DeathToll;

	public int DeathToll
	{
		get
		{
			return m_DeathToll;
		}
	}

	private GameData()
	{

	}

	public void TripPeople()
	{
		m_DickPercentage += m_DickBoostForTrippingPeople;

		if(m_DickPercentage > 1.0f)
		{
			m_DickPercentage = 1.0f;
		}

		m_DeathToll++;
	}

	public void UpdateScore(float deltaScore)
	{
		m_Score += deltaScore;
	}

	public void Update()
	{
		m_DickPercentage -= m_DickDecayRate * Time.deltaTime;

		if(m_DickPercentage < 0.0f)
		{
			m_DickPercentage = 0.0f;
		}
	}
}
