using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviourSingleton<SceneManager>
    {
        public void ChangeScene(int index)
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }

        public void ResetLevel()
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}

