using UnityEngine;
using Managers;

public class GameModeSetter : MonoBehaviour
{
    [SerializeField] GameModeData gameModeData;

    public void ChangeGameMode(bool soloMode)
    {
        if (soloMode)
        {
            gameModeData.gameMode = GameModeData.GameMode.Solo;
            SceneManager.Get().ChangeScene(2);
        }
        else
        {
            gameModeData.gameMode = GameModeData.GameMode.Multiplayer;
            SceneManager.Get().ChangeScene(1);
        }
    }
}
