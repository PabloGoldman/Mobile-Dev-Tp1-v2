using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejoPalletsSinglePlayer : MonoBehaviour
{
	protected System.Collections.Generic.List<Pallet> Pallets = new System.Collections.Generic.List<Pallet>();
	public ControladorDeDescargarSinglePlayer Controlador;
	protected int Contador = 0;

	public virtual bool Recibir(Pallet pallet)
	{
		Pallets.Add(pallet);
		pallet.Pasaje();
		return true;
	}

	public bool Tenencia()
	{
		if (Pallets.Count != 0)
			return true;
		else
			return false;
	}

	public virtual void Dar(ManejoPalletsSinglePlayer receptor)
	{
		//es el encargado de decidir si le da o no la bolsa
	}
}
