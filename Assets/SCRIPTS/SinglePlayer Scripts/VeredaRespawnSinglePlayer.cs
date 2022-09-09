using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeredaRespawnSinglePlayer : MonoBehaviour
{
	public string PlayerTag = "Player";

	// Use this for initialization
	void Start()
	{
		GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == PlayerTag)
		{
			other.GetComponent<RespawnSinglePlayer>().Respawnear();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == PlayerTag)
		{
			collision.gameObject.GetComponent<RespawnSinglePlayer>().Respawnear();
		}
	}
}
