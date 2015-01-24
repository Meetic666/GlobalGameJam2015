using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public float m_ItemSpawnTime;
	float m_ItemSpawnTimer;

	public float m_ExSpawnTime;
	float m_ExSpawnTimer;

	public float m_EnemySpawnTime;
	float m_EnemySpawnTimer;

	public float m_MaxHorizontalPosition;

	public GameObject m_ItemPrefab;
	public GameObject m_ExPrefab;
	public GameObject m_PeoplePrefab;

	public RunnersManager m_RunnersManager;
	public List<Player> m_Players;

	public LionPack m_LionPack;

	int m_MaxNumberOfObstacles = 5;
	
	public float m_DeathTime = 2.0f;
	float m_DeathTimer;


	void Start()
	{
		m_EnemySpawnTimer = m_EnemySpawnTime;
		m_ExSpawnTimer = m_ExSpawnTime;
		m_ItemSpawnTimer = m_ItemSpawnTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_ItemSpawnTimer > 0.0f)
		{
			m_ItemSpawnTimer -= Time.deltaTime;

			if(m_ItemSpawnTimer <= 0.0f)
			{
				SpawnItem ();

				m_ItemSpawnTimer = m_ItemSpawnTime;
			}
		}


		if(m_ExSpawnTimer > 0.0f)
		{
			m_ExSpawnTimer -= Time.deltaTime;
			
			if(m_ExSpawnTimer <= 0.0f)
			{
				SpawnEx ();

				m_ExSpawnTimer = m_ExSpawnTime;
			}
		}


		if(m_EnemySpawnTimer > 0.0f)
		{
			m_EnemySpawnTimer -= Time.deltaTime;
			
			if(m_EnemySpawnTimer <= 0.0f)
			{
				SpawnEnemy ();

				m_EnemySpawnTimer = m_EnemySpawnTime;
			}
		}

		GameData.Instance.Update ();		

		if(m_DeathTimer > 0.0f)
		{
			m_DeathTimer -= Time.deltaTime;
			
			if(m_DeathTimer <= 0.0f)
			{
				Application.LoadLevel("Summary");
			}
		}
		else
		{
			bool allPlayersDead = true;

			foreach(Player player in m_Players)
			{
				if(player.gameObject.activeSelf && player.CurrentState != BaseRunner.State.e_Carcass)
				{
					allPlayersDead = false;
				}
			}

			if(allPlayersDead)
			{
				m_DeathTimer = m_DeathTime;
			}
		}
	}

	void SpawnItem()
	{
		List<GameObject> newObjects = new List<GameObject> ();;

		Vector3 position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right;

		int index = 0;

		foreach(float dickPercentage in GameData.Instance.DickPercentages)
		{
			if(m_Players[index].gameObject.activeSelf && m_Players[index].CurrentState != BaseRunner.State.e_Carcass)
			{
				if(Random.value <= dickPercentage)
				{
					for(int i = 0; i < m_MaxNumberOfObstacles * dickPercentage; i++)
					{
						GameObject newObject = (GameObject)Instantiate(m_PeoplePrefab,position, Quaternion.identity);

						newObject.GetComponent<BaseRunner>().SetToCarcass();

						newObjects.Add (newObject);

						position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.forward;
					}
				}
			}
			else
			{
				newObjects.Add ((GameObject)Instantiate(m_ItemPrefab,position, Quaternion.identity));
			}

			index++;
		}

		foreach(GameObject newObject in newObjects)
		{
			newObject.transform.parent = m_RunnersManager.transform;
		}
	}

	void SpawnEx()
	{
		GameObject newObject;
		
		Vector3 position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right;

		newObject = (GameObject)Instantiate(m_ExPrefab,position, Quaternion.identity);
		newObject.GetComponent<People>().m_Players = m_Players;
		newObject.GetComponent<People>().m_LionPack = m_LionPack;
		
		newObject.transform.parent = m_RunnersManager.transform;
	}

	void SpawnEnemy()
	{
		GameObject newObject;
		
		Vector3 position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right;
		
		newObject = (GameObject)Instantiate(m_PeoplePrefab,position, Quaternion.identity);
		newObject.GetComponent<People>().m_Players = m_Players;
		newObject.GetComponent<People>().m_LionPack = m_LionPack;
		
		newObject.transform.parent = m_RunnersManager.transform;
	}
}
