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
        }
        else
        {
            gameModeData.gameMode = GameModeData.GameMode.Multiplayer;
        }

        SceneManager.Get().ChangeScene(1);
    }
}
