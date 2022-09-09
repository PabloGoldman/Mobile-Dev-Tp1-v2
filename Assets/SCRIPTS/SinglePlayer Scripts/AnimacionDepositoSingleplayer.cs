using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionDepositoSingleplayer : MonoBehaviour
{
	public string AnimEntrada = "Entrada";
	public string AnimSalida = "Salida";
	public ControladorDeDescargarSinglePlayer ContrDesc;

	enum AnimEnCurso { Salida, Entrada, Nada }
	AnimEnCurso AnimAct = AnimEnCurso.Nada;

	public GameObject PuertaAnimada;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
			Entrar();
		if (Input.GetKeyDown(KeyCode.X))
			Salir();

		switch (AnimAct)
		{
			case AnimEnCurso.Entrada:

				if (!GetComponent<Animation>().IsPlaying(AnimEntrada))
				{
					AnimAct = AnimEnCurso.Nada;
					ContrDesc.FinAnimEntrada();
				}
				break;

			case AnimEnCurso.Salida:
				if (!GetComponent<Animation>().IsPlaying(AnimSalida))
				{
					AnimAct = AnimEnCurso.Nada;
					ContrDesc.FinAnimSalida();
				}

				break;

			case AnimEnCurso.Nada:
				break;
		}
	}

	public void Entrar()
	{
		AnimAct = AnimEnCurso.Entrada;
		GetComponent<Animation>().Play(AnimEntrada);

		if (PuertaAnimada != null)
		{
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = 0;
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = 1;
			PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
		}
	}

	public void Salir()
	{
		AnimAct = AnimEnCurso.Salida;
		GetComponent<Animation>().Play(AnimSalida);

		if (PuertaAnimada != null)
		{
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].length;
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = -1;
			PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
		}
	}
}
