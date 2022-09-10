using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPointsManager : MonoBehaviour
{
    public float TiempEspReiniciar = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//PARA JUGAR
		if (Input.GetKeyDown(KeyCode.Space) ||
		   Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Alpha0))
		{
			SceneManager.Get().ChangeScene(0);
		}

		//CIERRA LA APLICACION
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		TiempEspReiniciar -= Time.deltaTime;
		if (TiempEspReiniciar <= 0)
		{
			SceneManager.Get().ChangeScene(0);
		}
	}
}
