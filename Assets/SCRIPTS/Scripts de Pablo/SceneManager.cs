using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneManager : MonoBehaviourSingleton<SceneManager>
    {
        public void ChangeScene(int index)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }

        public void ResetLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}

