using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Managers;
using System.Collections;

public class GameManagerSingleplayer : MonoBehaviour
{
    public abstract class GMState
    {
        public abstract void Update(GameManagerSingleplayer gm);
    }

    public class GMStateCalibrando : GMState
    {
        public override void Update(GameManagerSingleplayer gm)
        {
            foreach (Touch t in Input.touches)
            {
                gm.Player1.Seleccionado = true;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                gm.Player1.Seleccionado = true;
            }
        }
    }

    public class GMStateJugando : GMState
    {
        public override void Update(GameManagerSingleplayer gm)
        {
            gm.virtualJoystick.gameObject.SetActive(true);
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
        public override void Update(GameManagerSingleplayer gm)
        {
            gm.virtualJoystick.gameObject.SetActive(false);
            //muestra el puntaje

            gm.TiempEspMuestraPts -= Time.deltaTime;
            if (gm.TiempEspMuestraPts <= 0)
                Managers.SceneManager.Get().ChangeScene(4);
        }
    }

    GMState currentState = null;
    GMStateCalibrando stateCalibrando = new GMStateCalibrando();
    GMStateJugando stateJugando = new GMStateJugando();
    GMStateFinalizado stateFinalizado = new GMStateFinalizado();

    public static GameManagerSingleplayer Instancia;

    public SinglePlayerData playerData;

    [SerializeField] GameModeData gameData;
    [SerializeField] GameObject taxis;

    [SerializeField] VirtualJoystick virtualJoystick;

    public float TiempoDeJuego = 60;

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

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(currentState == stateJugando && !ConteoRedresivo);
    }

    //----------------------------------------------------------//

    public void IniciarTutorial()
    {
        currentState = stateCalibrando;

        playerData.finalScore = 0;

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
        Player1.GetComponent<FrenadoSinglePlayer>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera()
    {
        currentState = stateFinalizado;

        TiempoDeJuego = 0;

        playerData.finalScore = Player1.Dinero;

        Player1.GetComponent<FrenadoSinglePlayer>().Frenar();

        Player1.ContrDesc.FinDelJuego();
    }

    //cambia a modo de carrera
    void CambiarACarrera()
    {
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

        Player1.gameObject.transform.position = PosCamionesCarrera[0];

        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<FrenadoSinglePlayer>().Frenar();
        Player1.CambiarAConduccion();

        //los deja andando
        Player1.GetComponent<FrenadoSinglePlayer>().RestaurarVel();
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
