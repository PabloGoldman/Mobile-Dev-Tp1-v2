using UnityEngine;
using TMPro;

public class GameModeSetter : MonoBehaviour
{
    [SerializeField] GameModeData gameModeData;

    [SerializeField] TextMeshProUGUI gameModeText;

    private void Start()
    {
        gameModeData.gameMode = GameModeData.GameMode.Easy;
    }

    public void ChangeGameMode()
    {
        switch (gameModeData.gameMode)
        {
            case GameModeData.GameMode.Easy:
                gameModeData.gameMode = GameModeData.GameMode.Medium;
                gameModeText.text = "Medium";
                break;
            case GameModeData.GameMode.Medium:
                gameModeData.gameMode = GameModeData.GameMode.Hard;
                gameModeText.text = "Hard";
                break;
            case GameModeData.GameMode.Hard:
                gameModeData.gameMode = GameModeData.GameMode.Easy;
                gameModeText.text = "Easy";
                break;
            default:
                break;
        }
    }
}
