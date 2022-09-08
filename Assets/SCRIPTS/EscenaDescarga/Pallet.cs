using UnityEngine;
using System.Collections;

public class Pallet : MonoBehaviour
{
    public Valores Valor;
    public float Tiempo;
    public GameObject CintaReceptora = null;
    public GameObject Portador = null;
    public float TiempEnCinta = 1.5f;
    public float TempoEnCinta = 0;

    public enum Valores
    {
        Valor1 = 100000,
        Valor2 = 250000,
        Valor3 = 500000
    }


    public float TiempSmoot = 0.3f;
    float TempoSmoot = 0;
    public bool EnSmoot = false;

    //----------------------------------------------//

    void Start()
    {
        Pasaje();
    }

    void LateUpdate()
    {
        if (Portador != null)
        {
            if (EnSmoot)
            {
                TempoSmoot += T.GetDT();
                if (TempoSmoot >= TiempSmoot)
                {
                    EnSmoot = false;
                    TempoSmoot = 0;
                }
            }
            else
            {
                transform.position = Portador.transform.position;
            }
        }

    }

    //----------------------------------------------//

    public void Pasaje()
    {
        EnSmoot = true;
        TempoSmoot = 0;
    }
}
