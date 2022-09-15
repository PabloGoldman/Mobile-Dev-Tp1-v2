using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets
{
    [SerializeField] bool leftPallet;

    public MoveType miInput;
    public enum MoveType
    {
        WASD,
        Arrows
    }

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;
    bool primeroCompleto = false;

    int amountOfTouches;
    int touchesToChange = 1;

    private void OnDisable()
    {
        primeroCompleto = false;
        segundoCompleto = false;
    }

    private void Update()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                if (leftPallet)
                {
                    if (t.position.x < Screen.width / 2)
                    {
                        AddTouches();
                    }
                }
                else
                {
                    if (t.position.x > Screen.width / 2)
                    {
                        AddTouches();
                    }
                }
            }
        }

        switch (miInput)
        {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A) || amountOfTouches >= touchesToChange && !primeroCompleto)
                {
                    amountOfTouches = 0;
                    PrimerPaso();

                    primeroCompleto = true;
                    segundoCompleto = false;
                }
                if (Tenencia() && (Input.GetKeyDown(KeyCode.S) || amountOfTouches >= touchesToChange) && !segundoCompleto)
                {
                    amountOfTouches = 0;
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && (Input.GetKeyDown(KeyCode.D) || amountOfTouches >= touchesToChange))
                {
                    amountOfTouches = 0;
                    TercerPaso();
                    primeroCompleto = false;
                }
                break;
            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow) || amountOfTouches >= touchesToChange && !primeroCompleto)
                {
                    amountOfTouches = 0;
                    PrimerPaso();
                    primeroCompleto = true;
                    segundoCompleto = false;
                }
                if (Tenencia() && (Input.GetKeyDown(KeyCode.DownArrow) || amountOfTouches >= touchesToChange) && !segundoCompleto)
                {
                    amountOfTouches = 0;
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow) || amountOfTouches >= touchesToChange)
                {
                    amountOfTouches = 0;
                    TercerPaso();
                    primeroCompleto = false;
                }
                break;
            default:
                break;
        }
    }

    public void AddTouches()
    {
        amountOfTouches++;
        Debug.Log(amountOfTouches);
    }

    void PrimerPaso()
    {
        Debug.Log("Primer Paso");
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso()
    {
        Debug.Log("Segundo Paso");
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso()
    {
        Debug.Log("Tercer Paso");
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
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
