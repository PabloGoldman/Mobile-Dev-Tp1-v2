using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Managers;

public class MngPts : MonoBehaviour
{
    public float TiempEmpAnims = 2.5f;

    public Image player1WinnerImage;
    public Image player2WinnerImage;

    public TextMeshProUGUI finalPointsPlayer1;
    public TextMeshProUGUI finalPointsPlayer2;

    public float TiempEspReiniciar = 10;

    //---------------------------------//

    // Use this for initialization
    void Start()
    {
        SetGanador();
    }

    // Update is called once per frame
    void Update()
    {
        if (TiempEspReiniciar < 5)
        {
            foreach (Touch t in Input.touches)
            {
                Managers.SceneManager.Get().ChangeScene(0);
            }
        }

        //PARA JUGAR
        if (Input.GetKeyDown(KeyCode.Space) ||
           Input.GetKeyDown(KeyCode.Return) ||
           Input.GetKeyDown(KeyCode.Alpha0))
        {
            Managers.SceneManager.Get().ChangeScene(0);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        TiempEspReiniciar -= Time.deltaTime;
        if (TiempEspReiniciar <= 0)
        {
            Managers.SceneManager.Get().ChangeScene(0);
        }
    }
    //---------------------------------//

    void SetGanador()
    {
        switch (DatosPartida.LadoGanadaor)
        {
            case DatosPartida.Lados.Izq:
                player1WinnerImage.gameObject.SetActive(true);
                finalPointsPlayer1.text = "$" + DatosPartida.PtsGanador;
                finalPointsPlayer2.text = "$" + DatosPartida.PtsPerdedor;
                break;

            case DatosPartida.Lados.Der:
                player2WinnerImage.gameObject.SetActive(true);
                finalPointsPlayer2.text = "$" + DatosPartida.PtsGanador;
                finalPointsPlayer1.text = "$" + DatosPartida.PtsPerdedor;
                break;
        }
    }
}
