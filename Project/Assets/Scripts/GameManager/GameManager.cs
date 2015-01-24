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
	public Player m_Player;

	int m_MaxNumberOfObstacles = 5;


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
	}

	void SpawnItem()
	{
		List<GameObject> newObjects = new List<GameObject> ();;

		Vector3 position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right;

		if(Random.value <= GameData.Instance.DickPercentage)
		{
			for(int i = 0; i < m_MaxNumberOfObstacles * GameData.Instance.DickPercentage; i++)
			{
				GameObject newObject = (GameObject)Instantiate(m_PeoplePrefab,position, Quaternion.identity);

				newObject.GetComponent<BaseRunner>().SetToCarcass();

				newObjects.Add (newObject);

				position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.forward;
			}
		}
		else
		{
			newObjects.Add ((GameObject)Instantiate(m_ItemPrefab,position, Quaternion.identity));
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
		newObject.GetComponent<People>().m_Player = m_Player;
		
		newObject.transform.parent = m_RunnersManager.transform;
	}

	void SpawnEnemy()
	{
		GameObject newObject;
		
		Vector3 position = transform.position + Random.Range (-1.0f, 1.0f) * m_MaxHorizontalPosition * transform.right;
		
		newObject = (GameObject)Instantiate(m_PeoplePrefab,position, Quaternion.identity);
		newObject.GetComponent<People>().m_Player = m_Player;
		
		newObject.transform.parent = m_RunnersManager.transform;
	}
}
