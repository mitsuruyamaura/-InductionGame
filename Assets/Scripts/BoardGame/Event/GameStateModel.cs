using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using yamap_BoardGame;

public class GameStateModel : MonoBehaviour
{
    public ReactiveProperty<GameState> CurrentGameState = new();
}
