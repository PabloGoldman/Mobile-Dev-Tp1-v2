using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenadoSinglePlayer : MonoBehaviour
{
	public float VelEntrada = 0;
	public string TagDeposito = "Deposito";

	int Contador = 0;
	int CantMensajes = 10;
	float TiempFrenado = 0.5f;
	float Tempo = 0f;

	Vector3 Destino;

	public bool Frenando = false;

	//-----------------------------------------------------//

	// Use this for initialization
	void Start()
	{
		Frenar();
	}

	void FixedUpdate()
	{
		if (Frenando)
		{
			Tempo += T.GetFDT();
			if (Tempo >= (TiempFrenado / CantMensajes) * Contador)
			{
				Contador++;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == TagDeposito)
		{
			DepositoSinglePlayer dep = other.GetComponent<DepositoSinglePlayer>();
			if (dep.Vacio)
			{
				if (this.GetComponent<PlayerSinglePlayer>().ConBolasas())
				{
					dep.Entrar(this.GetComponent<PlayerSinglePlayer>());
					Destino = other.transform.position;
					transform.forward = Destino - transform.position;
					Frenar();
				}
			}
		}
	}

	//-----------------------------------------------------------//

	public void Frenar()
	{
		GetComponent<ControlDireccion>().enabled = false;
		gameObject.GetComponent<CarController>().SetAcel(0f);
		GetComponent<Rigidbody>().velocity = Vector3.zero;

		Frenando = true;
		Tempo = 0;
		Contador = 0;
	}

	public void RestaurarVel()
	{
		GetComponent<ControlDireccion>().enabled = true;
		gameObject.GetComponent<CarController>().SetAcel(1f);
		Frenando = false;
		Tempo = 0;
		Contador = 0;
	}
}
