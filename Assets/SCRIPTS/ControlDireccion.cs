using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
    public enum TipoInput { AWSD, Arrows }
    public TipoInput InputAct = TipoInput.AWSD;

    float Giro = 0;

    public bool Habilitado = true;
    CarController carController;
    int playerID = -1;
    string inputName = "Horizontal";

    //---------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();

        if (GetComponent<Player>())
        {
            playerID = GetComponent<Player>().IdPlayer;
        }

        else if (GetComponent<PlayerSinglePlayer>())
        {
            playerID = GetComponent<PlayerSinglePlayer>().IdPlayer;
        }

        inputName += playerID;
    }

    // Update is called once per frame
    void Update()
    {
        //switch (InputAct)
        //{
        //    case TipoInput.AWSD:
        //        if (Habilitado)
        //        {
        //            if (Input.GetKey(KeyCode.A))
        //            {
        //                Giro = -1;
        //            }
        //            else if (Input.GetKey(KeyCode.D))
        //            {
        //                Giro = 1;
        //            }
        //            else
        //            {
        //                Giro = 0;
        //            }
        //        }
        //        break;
        //    case TipoInput.Arrows:
        //        if (Habilitado)
        //        {
        //            if (Input.GetKey(KeyCode.LeftArrow))
        //            {
        //                Giro = -1;
        //            }
        //            else if (Input.GetKey(KeyCode.RightArrow))
        //            {
        //                Giro = 1;
        //            }
        //            else
        //            {
        //                Giro = 0;
        //            }
        //        }
        //        break;
        //}

        Giro = Input.GetAxis(inputName);

        carController.SetGiro(Giro);
    }

    public float GetGiro()
    {
        return Giro;
    }

}
