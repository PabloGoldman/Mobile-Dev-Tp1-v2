using Managers;
using UnityEngine;

public class FinalPointsManager : MonoBehaviour
{
    public float TiempEspReiniciar = 10;

    // Update is called once per frame
    void Update()
    {
		foreach (Touch t in Input.touches)
		{
			SceneManager.Get().ChangeScene(0);
		}

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
