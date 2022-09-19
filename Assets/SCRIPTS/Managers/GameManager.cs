using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Managers;

public class GameManager : MonoBehaviour
{
    public abstract class GMState
    {
        public abstract void Update(GameManager gm);
    }

    public class GMStateCalibrando : GMState
    {
        public override void Update(GameManager gm)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.position.x < Screen.width / 2)
                {
                    gm.Player1.Seleccionado = true;
                }
                else
                {
                    gm.Player2.Seleccionado = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                gm.Player1.Seleccionado = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                gm.Player2.Seleccionado = true;
            }
        }
    }

    public class GMStateJugando : GMState
    {
        public override void Update(GameManager gm)
        {
            foreach (VirtualJoystick vj in gm.virtualJoystick)
            {
                vj.gameObject.SetActive(true);
            }
            //SKIP LA CARRERA
            if (Input.GetKey(KeyCode.Alpha9))
            {
                gm.TiempoDeJuego = 0;
            }

            if (gm.TiempoDeJuego <= 0)
            {
                gm.FinalizarCarrera();
            }

            if (gm.ConteoRedresivo)
            {
                gm.ConteoParaInicion -= T.GetDT();
                if (gm.ConteoParaInicion < 0)
                {
                    gm.EmpezarCarrera();
                    gm.ConteoRedresivo = false;
                }
            }
            else
            {
                //baja el tiempo del juego
                gm.TiempoDeJuego -= T.GetDT();
            }
            if (gm.ConteoRedresivo)
            {
                if (gm.ConteoParaInicion > 1)
                {
                    gm.ConteoInicio.text = gm.ConteoParaInicion.ToString("0");
                }
                else
                {
                    gm.ConteoInicio.text = "GO";
                }
            }

            gm.ConteoInicio.gameObject.SetActive(gm.ConteoRedresivo);

            gm.TiempoDeJuegoText.text = gm.TiempoDeJuego.ToString("00");
        }
    }

    public class GMStateFinalizado : GMState
    {
        public override void Update(GameManager gm)
        {
            foreach (VirtualJoystick vj in gm.virtualJoystick)
            {
                vj.gameObject.SetActive(false);
            }
            //muestra el puntaje

            gm.TiempEspMuestraPts -= Time.deltaTime;
            if (gm.TiempEspMuestraPts <= 0)
                SceneManager.Get().ChangeScene(3);
        }
    }

    GMState currentState = null;
    GMStateCalibrando stateCalibrando = new GMStateCalibrando();
    GMStateJugando stateJugando = new GMStateJugando();
    GMStateFinalizado stateFinalizado = new GMStateFinalizado();

    public static GameManager Instancia;

    [SerializeField] GameModeData gameData;
    [SerializeField] GameObject taxis;

    [SerializeField] VirtualJoystick[] virtualJoystick;

    public float TiempoDeJuego = 60;

    //public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    //public EstadoJuego EstAct = EstadoJuego.Calibrando;

    public Player Player1;
    public Player Player2;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public Text ConteoInicio;
    public Text TiempoDeJuegoText;

    public float TiempEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];
    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion1;
    public GameObject[] ObjsCalibracion2;
    //la pista de carreras
    public GameObject[] ObjsCarrera;

    //--------------------------------------------------------//

    void Awake()
    {
        Instancia = this;
    }

    IEnumerator Start()
    {
        yield return null;
        currentState = stateCalibrando;
        IniciarTutorial();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this);
        }
        //REINICIAR
        if (Input.GetKey(KeyCode.Alpha0))
        {
            SceneManager.Get().ResetLevel();
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(currentState == stateJugando && !ConteoRedresivo);
    }

    //----------------------------------------------------------//

    public void IniciarTutorial()
    {
        currentState = stateCalibrando;

        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(true);
            ObjsCalibracion2[i].SetActive(true);
        }

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(false);
        }

        Player1.CambiarATutorial();
        Player2.CambiarATutorial();

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera()
    {
        //EstAct = GameManager.EstadoJuego.Finalizado;
        currentState = stateFinalizado;

        TiempoDeJuego = 0;

        if (Player1.Dinero > Player2.Dinero)
        {
            //lado que gano
            if (Player1.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
            //puntajes
            DatosPartida.PtsGanador = Player1.Dinero;
            DatosPartida.PtsPerdedor = Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (Player2.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player2.Dinero;
            DatosPartida.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Frenado>().Frenar();
        Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        Player2.ContrDesc.FinDelJuego();
    }

    //cambia a modo de carrera
    void CambiarACarrera()
    {
        //EstAct = EstadoJuego.Jugando;

        currentState = stateJugando;

        switch (gameData.gameMode)
        {
            case GameModeData.GameMode.Easy:
                ObjsCarrera[1].SetActive(true);
                ObjsCarrera[2].SetActive(true);
                break;
            case GameModeData.GameMode.Medium:
                for (int i = 0; i < ObjsCarrera.Length; i++)
                {
                    ObjsCarrera[i].SetActive(true);
                    taxis.SetActive(false);
                }
                break;
            case GameModeData.GameMode.Hard:

                for (int i = 0; i < ObjsCarrera.Length; i++)
                {
                    ObjsCarrera[i].SetActive(true);
                }
                break;
            default:
                break;
        }

        //desactivacion de la calibracion
        Player1.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion1.Length; i++)
        {
            ObjsCalibracion1[i].SetActive(false);
        }

        Player2.FinCalibrado = true;

        for (int i = 0; i < ObjsCalibracion2.Length; i++)
        {
            ObjsCalibracion2[i].SetActive(false);
        }

        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (Player1.LadoActual == Visualizacion.Lado.Izq)
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[0];
            Player2.gameObject.transform.position = PosCamionesCarrera[1];
        }
        else
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[1];
            Player2.gameObject.transform.position = PosCamionesCarrera[0];
        }

        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Frenado>().Frenar();
        Player1.CambiarAConduccion();

        Player2.transform.forward = Vector3.forward;
        Player2.GetComponent<Frenado>().Frenar();
        Player2.CambiarAConduccion();

        //los deja andando
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<Frenado>().RestaurarVel();
        //cancela la direccion
        Player1.GetComponent<ControlDireccion>().Habilitado = false;
        Player2.GetComponent<ControlDireccion>().Habilitado = false;
        //les de direccion
        Player1.transform.forward = Vector3.forward;
        Player2.transform.forward = Vector3.forward;

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            Player1.FinTuto = true;

        }
        if (playerID == 1)
        {
            Player2.FinTuto = true;
        }

        if (Player1.FinTuto && Player2.FinTuto)
            CambiarACarrera();
    }

}
