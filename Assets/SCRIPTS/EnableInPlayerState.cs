using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInPlayerState : MonoBehaviour
{
    public Player.Estados[] MisEstados;
    public PlayerSinglePlayer.Estados[] MisEstadosSinglePlayer;

    Player.Estados prevEstado = Player.Estados.Ninguno;
    PlayerSinglePlayer.Estados prevEstadoSinglePlayer = PlayerSinglePlayer.Estados.Ninguno;


    public void SetPlayerState(Player.Estados state) {
        if (prevEstado != state) {
            bool activo = false;
            foreach (var estados in MisEstados) {
                if (estados == state) {
                    activo = true;
                    break;
                }
            }
            gameObject.SetActive(activo);
            prevEstado = state;
        }
        
    }

    public void SetPlayerStateSinglePlayer(PlayerSinglePlayer.Estados state)
    {
        if (prevEstadoSinglePlayer != state)
        {
            bool activo = false;
            foreach (var estados in MisEstadosSinglePlayer)
            {
                if (estados == state)
                {
                    activo = true;
                    break;
                }
            }
            gameObject.SetActive(activo);
            prevEstadoSinglePlayer = state;
        }

    }
}
