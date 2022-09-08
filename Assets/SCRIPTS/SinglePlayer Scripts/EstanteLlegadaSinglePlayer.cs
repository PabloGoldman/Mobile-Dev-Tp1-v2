using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstanteLlegadaSinglePlayer : ManejoPalletsSinglePlayer
{
    public GameObject Mano;
    public ContrCalibracionSinglePlayer ContrCalib;

    public override bool Recibir(Pallet p)
    {
        p.Portador = this.gameObject;
        base.Recibir(p);
        ContrCalib.FinTutorial();

        return true;
    }
}
