using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	List<bool> m_PlayersJoined = new List<bool> ();

	public List<bool> PlayersJoined
	{
		get
		{
			return m_PlayersJoined;
		}
	}

	List<bool> m_PlayersConfirmed = new List<bool> ();
	
	public List<bool> PlayersConfirmed
	{
		get
		{
			return m_PlayersConfirmed;
		}
	}

	public int NumberOfPlayersJoined
	{
		get
		{
			int result = 0;

			for(int i = 0; i < m_PlayersJoined.Count; i++)
			{
				if(m_PlayersJoined[i])
				{
					result++;
				}
			}

			return result;
		}
	}

	public int NumberOfPlayersConfirmed
	{
		get
		{
			int result = 0;
			
			for(int i = 0; i < m_PlayersConfirmed.Count; i++)
			{
				if(m_PlayersConfirmed[i])
				{
					result++;
				}
			}
			
			return result;
		}
	}

	List<float> m_DickPercentages = new List<float>(4);

	public List<float> DickPercentages 
	{
		get
		{
			return m_DickPercentages;
		}
	}
	
	
	float m_DickBoostForTrippingPeople = 0.1f;
	float m_DickDecayRate = 0.01f;

	List<float> m_Scores = new List<float>(4);

	public List<float> Scores
	{
		get
		{
			return m_Scores;
		}
	}

	List<int> m_DeathTolls = new List<int>(4);

	public List<int> DeathTolls
	{
		get
		{
			return m_DeathTolls;
		}
	}

	List<int> m_NumberOfButtonMashes = new List<int>(4);

	public List<int> NumberOfButtonMashes
	{
		get
		{
			return m_NumberOfButtonMashes;
		}
	}

	private GameData()
	{
		for(int i = 0; i < 4; i++)
		{
			m_DickPercentages.Add (0.0f);
			m_Scores.Add(0.0f);
			m_DeathTolls.Add(0);
			m_PlayersJoined.Add (false);
			m_PlayersConfirmed.Add (false);
			m_NumberOfButtonMashes.Add (0);
		}
	}

	public void TripPeople(int playerNumber)
	{
		m_DickPercentages[playerNumber - 1] += m_DickBoostForTrippingPeople;

		if(m_DickPercentages[playerNumber - 1] > 1.0f)
		{
			m_DickPercentages[playerNumber - 1] = 1.0f;
		}

		m_DeathTolls[playerNumber - 1]++;
	}

	public void UpdateScore(float deltaScore, int playerNumber)
	{
		m_Scores[playerNumber - 1] += deltaScore;
	}

	public void Update()
	{
		for(int i = 0; i < m_DickPercentages.Count; i++)
		{
			m_DickPercentages[i] -= m_DickDecayRate * Time.deltaTime;

			if(m_DickPercentages[i] < 0.0f)
			{
				m_DickPercentages[i] = 0.0f;
			}
		}
	}

	public void JoinPlayer(int playerNumber, bool isJoined)
	{
		m_PlayersJoined [playerNumber - 1] = isJoined;
	}

	public void ConfirmPlayer(int playerNumber, bool isConfirmed)
	{
		m_PlayersConfirmed [playerNumber - 1] = isConfirmed;
	}

	public void ResetPlayersConfirmed()
	{
		for(int i = 0; i < m_PlayersConfirmed.Count; i++)
		{
			m_PlayersConfirmed[i] = false;
		}
	}

	public void AddButtonMash(int playerNumber)
	{
		m_NumberOfButtonMashes [playerNumber - 1]++;
	}
}
