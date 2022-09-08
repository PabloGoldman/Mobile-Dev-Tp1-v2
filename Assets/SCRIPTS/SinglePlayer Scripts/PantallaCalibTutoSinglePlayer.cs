using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaCalibTutoSinglePlayer : MonoBehaviour
{
	public Texture2D[] ImagenesDelTuto;
	public float Intervalo = 1.2f;//tiempo de cada cuanto cambia de imagen
	float TempoIntTuto = 0;
	int EnCursoTuto = 0;

	public Texture2D[] ImagenesDeCalib;
	int EnCursoCalib = 0;
	float TempoIntCalib = 0;

	public Texture2D ImaReady;

	public ContrCalibracionSinglePlayer ContrCalib;

	// Update is called once per frame
	void Update()
	{
		switch (ContrCalib.EstAct)
		{
			case ContrCalibracionSinglePlayer.Estados.Calibrando:
				//pongase en posicion para iniciar
				TempoIntCalib += T.GetDT();
				if (TempoIntCalib >= Intervalo)
				{
					TempoIntCalib = 0;
					if (EnCursoCalib + 1 < ImagenesDeCalib.Length)
						EnCursoCalib++;
					else
						EnCursoCalib = 0;
				}
				GetComponent<Renderer>().material.mainTexture = ImagenesDeCalib[EnCursoCalib];
				break;

			case ContrCalibracionSinglePlayer.Estados.Tutorial:
				//tome la bolsa y depositela en el estante
				TempoIntTuto += T.GetDT();
				if (TempoIntTuto >= Intervalo)
				{
					TempoIntTuto = 0;
					if (EnCursoTuto + 1 < ImagenesDelTuto.Length)
						EnCursoTuto++;
					else
						EnCursoTuto = 0;
				}
				GetComponent<Renderer>().material.mainTexture = ImagenesDelTuto[EnCursoTuto];
				break;

			case ContrCalibracionSinglePlayer.Estados.Finalizado:
				//esperando al otro jugador		
				GetComponent<Renderer>().material.mainTexture = ImaReady;
				break;
		}
	}
}
