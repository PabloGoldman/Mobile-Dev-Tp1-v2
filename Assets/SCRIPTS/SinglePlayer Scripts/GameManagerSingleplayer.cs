using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class GameManagerSingleplayer : MonoBehaviour
{
    public static GameManagerSingleplayer Instancia;

    [SerializeField] GameModeData gameData;

    public float TiempoDeJuego = 60;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    public PlayerSinglePlayer Player1;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public Text ConteoInicio;
    public Text TiempoDeJuegoText;

    public float TiempEspMuestraPts = 3;

    public Vector3[] PosCamionesCarrera = new Vector3[2];
    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion1;
    //la pista de carreras
    public GameObject[] ObjsCarrera;

    //--------------------------------------------------------//

    void Awake()
    {
        GameManagerSingleplayer.Instancia = this;
    }

    IEnumerator Start()
    {
        yield return null;
        IniciarTutorial();
    }

    void Update()
    {
        var touches = Input.touches;

        string log = "";

        foreach (var t in touches)
        {
            log += $"{t.fingerId} - {t.position} ";
        }


        //REINICIAR
        if (Input.GetKey(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:

                if (Input.GetKeyDown(KeyCode.W))
                {
                    Player1.Seleccionado = true;
                }
                break;


            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Alpha9))
                {
                    TiempoDeJuego = 0;
                }

                if (TiempoDeJuego <= 0)
                {
                    FinalizarCarrera();
                }

                if (ConteoRedresivo)
                {
                    ConteoParaInicion -= T.GetDT();
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else
                {
                    //baja el tiempo del juego
                    TiempoDeJuego -= T.GetDT();
                }
                if (ConteoRedresivo)
                {
                    if (ConteoParaInicion > 1)
                    {
                        ConteoInicio.text = ConteoParaInicion.ToString("0");
                    }
                    else
                    {
                        ConteoInicio.text = "GO";
                    }
                }

                ConteoInicio.gameObject.SetActive(ConteoRedresivo);

                TiempoDeJuegoText.text = TiempoDeJuego.ToString("00");

                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !ConteoRedresivo);
    }

    //----------------------------------------------------------//

    public void IniciarTutorial()
    {
        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(true);
        }

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(false);
        }

        Player1.CambiarATutorial();

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera()
    {
        EstAct = GameManagerSingleplayer.EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        //lado que gano
        if (Player1.LadoActual == Visualizacion.Lado.Der)
            DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
        else
            DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
        //puntajes
        DatosPartida.PtsGanador = Player1.Dinero;


        Player1.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
    }

    //cambia a modo de carrera
    void CambiarACarrera()
    {

        EstAct = GameManagerSingleplayer.EstadoJuego.Jugando;

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(true);
        }

        //desactivacion de la calibracion
        Player1.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(false);
        }

        Player1.gameObject.transform.position = PosCamionesCarrera[0];

        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Frenado>().Frenar();
        Player1.CambiarAConduccion();

        //los deja andando
        Player1.GetComponent<Frenado>().RestaurarVel();
        //cancela la direccion
        Player1.GetComponent<ControlDireccion>().Habilitado = false;
        //les de direccion
        Player1.transform.forward = Vector3.forward;

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            Player1.FinTuto = true;
        }

        if (Player1.FinTuto)
            CambiarACarrera();
    }
}
