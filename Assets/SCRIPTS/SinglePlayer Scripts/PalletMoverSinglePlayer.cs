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
            //if (t.position.x < Screen.width / 2 && contador <= 0f && speed != 0f && PauseMenu.GameIsPaused == false)
            //{
            //    if (!GameManager.self.winGame)
            //    {
            //        speed = -speed;
            //        contador = 80f;
            //        transform.RotateAround(transform.position, transform.up, 180f);
            //    }
            //}

            amountOfTouches++;
        }

        if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A) || amountOfTouches >= 5)
        {
            Debug.Log("Primer paso done");
            PrimerPaso();
            amountOfTouches = 0;
        }
        if (Tenencia() && Input.GetKeyDown(KeyCode.S) || amountOfTouches >= 5)
        {
            Debug.Log("Segundo paso done");
            SegundoPaso();
            amountOfTouches = 0;
        }
        if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D) || amountOfTouches >= 5)
        {
            Debug.Log("Tercer paso done");
            TercerPaso();
            amountOfTouches = 0;
        }
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
