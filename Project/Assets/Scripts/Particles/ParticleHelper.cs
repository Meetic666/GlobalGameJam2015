using UnityEngine;
using System.Collections;

public class ParticleHelper : MonoBehaviour {


	public static ParticleHelper Instance;

	public ParticleSystem Punch;
	public ParticleSystem Trip;
	public ParticleSystem Energy;


	// Use this for initialization
	void Start () {

		Instance = this;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public ParticleSystem PunchExplosion(Vector3 position)
	{
		return instantiate (Punch, position);
	}

	public ParticleSystem TrippedSwirl (Vector3 position)
	{
		return instantiate (Trip, position);

	}

	public ParticleSystem FunEnergy (Vector3 position)
	{
		return instantiate (Energy, position);
	}


	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate (prefab, position, Quaternion.identity) as ParticleSystem;

		Destroy (newParticleSystem.gameObject,
		        newParticleSystem.startLifetime);

		return newParticleSystem;

	}


}
