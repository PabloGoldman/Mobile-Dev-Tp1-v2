using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameModeData")]
public class GameModeData : ScriptableObject
{
    public enum GameMode { Easy, Medium, Hard};

    public GameMode gameMode;
}
