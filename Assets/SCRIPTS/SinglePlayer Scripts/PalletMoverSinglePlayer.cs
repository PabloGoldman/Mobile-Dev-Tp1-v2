using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMoverSinglePlayer : ManejoPalletsSinglePlayer
{
    public MoveType miInput;
    public enum MoveType
    {
        WASD,
        Arrows
    }

    //SON LAS ANIMACIONES DEL PRINCIPIO // TUTORIAL
    public ManejoPalletsSinglePlayer Desde, Hasta;
    bool segundoCompleto = false;

    int amountOfTouches;

    private void Update()
    {
        foreach (Touch t in Input.touches)
        {
            AddTouches();
        }

        if (!Tenencia() && Desde.Tenencia() && (Input.GetKeyDown(KeyCode.A) || amountOfTouches >= 5))
        {
            Debug.Log("Primer paso done");
            amountOfTouches = 0;
            PrimerPaso();
        }
        if (Tenencia() && (Input.GetKeyDown(KeyCode.S) || amountOfTouches >= 5) && segundoCompleto == false)
        {
            Debug.Log("Segundo paso done");
            amountOfTouches = 0;
            SegundoPaso();
        }
        if (segundoCompleto && Tenencia() && (Input.GetKeyDown(KeyCode.D) || amountOfTouches >= 5))
        {
            Debug.Log("Tercer paso done");
            amountOfTouches = 0;
            TercerPaso();
        }
    }

    public void AddTouches()
    {
        amountOfTouches++;
        Debug.Log(amountOfTouches);
        //PATRON OBSERVER
    }

    void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso()
    {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPalletsSinglePlayer receptor)
    {
        if (Tenencia())
        {
            if (receptor.Recibir(Pallets[0]))
            {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet)
    {
        if (!Tenencia())
        {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
