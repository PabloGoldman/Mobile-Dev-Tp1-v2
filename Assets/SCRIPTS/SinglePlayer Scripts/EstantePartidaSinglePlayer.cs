using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstantePartidaSinglePlayer : ManejoPalletsSinglePlayer
{
	public GameObject ManoReceptora;

	void OnTriggerEnter(Collider other)
	{
		ManejoPalletsSinglePlayer recept = other.GetComponent<ManejoPalletsSinglePlayer>();
		if (recept != null)
		{
			Dar(recept);
		}
	}

	//------------------------------------------------------------//

	public override void Dar(ManejoPalletsSinglePlayer receptor)
	{
		if (receptor.Recibir(Pallets[0]))
		{
			Pallets.RemoveAt(0);
		}
	}

	public override bool Recibir(Pallet pallet)
	{
		pallet.Portador = gameObject;
		return base.Recibir(pallet);
	}
}
