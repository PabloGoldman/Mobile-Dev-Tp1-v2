using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSinglePlayer : MonoBehaviour
{
	public int Dinero = 0;
	public int IdPlayer = 0;

	public BolsaSinglePlayer[] Bolasas;
	int CantBolsAct = 0;
	public string TagBolsas = "";

	public enum Estados { EnDescarga, EnConduccion, EnTutorial, Ninguno }
	public Estados EstAct = Estados.EnConduccion;

	public bool EnConduccion = true;
	public bool EnDescarga = false;

	public ControladorDeDescargarSinglePlayer ContrDesc;
	public ContrCalibracionSinglePlayer ContrCalib;

	VisualizacionSinglePlayer MiVisualizacion;

	public bool Seleccionado = false;
	public bool FinCalibrado = false;
	public bool FinTuto = false;

	//public VisualizacionSinglePlayer.Lado LadoActual => VisualizacionSinglePlayer.LadoAct;

	//------------------------------------------------------------------//

	void Start()
	{
		for (int i = 0; i < Bolasas.Length; i++)
			Bolasas[i] = null;

		MiVisualizacion = GetComponent<VisualizacionSinglePlayer>();
	}

	//------------------------------------------------------------------//

	public bool AgregarBolsa(BolsaSinglePlayer b)
	{
		if (CantBolsAct + 1 <= Bolasas.Length)
		{
			Bolasas[CantBolsAct] = b;
			CantBolsAct++;
			Dinero += (int)b.Monto;
			b.Desaparecer();
			return true;
		}
		else
		{
			return false;
		}
	}

	public void VaciarInv()
	{
		for (int i = 0; i < Bolasas.Length; i++)
			Bolasas[i] = null;

		CantBolsAct = 0;
	}

	public bool ConBolasas()
	{
		for (int i = 0; i < Bolasas.Length; i++)
		{
			if (Bolasas[i] != null)
			{
				return true;
			}
		}
		return false;
	}

	public void SetContrDesc(ControladorDeDescargarSinglePlayer contr)
	{
		ContrDesc = contr;
	}

	public ControladorDeDescargarSinglePlayer GetContr()
	{
		return ContrDesc;
	}

	public void CambiarATutorial()
	{
		EstAct = Estados.EnTutorial;
		MiVisualizacion.CambiarATutorial();
	}

	public void CambiarAConduccion()
	{
		EstAct = Estados.EnConduccion;
		MiVisualizacion.CambiarAConduccion();
	}

	public void CambiarADescarga()
	{
		EstAct = Estados.EnDescarga;
		MiVisualizacion.CambiarADescarga();
	}

	public void SacarBolasa()
	{
		for (int i = 0; i < Bolasas.Length; i++)
		{
			if (Bolasas[i] != null)
			{
				Bolasas[i] = null;
				return;
			}
		}
	}
}
