using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
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
        Giro = InputManager.Get().GetAxis(inputName);

        carController.SetGiro(Giro);
    }

    public float GetGiro()
    {
        return Giro;
    }

}
