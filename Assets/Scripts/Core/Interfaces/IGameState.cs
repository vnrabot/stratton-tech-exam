using System.Collections.Generic;
using UnityEngine;

//readonly state of the Game for the UI to read data from
public interface IGameState
{
    IEnumerable<Vector2Int> GetAliveCells();
    int CurrentGeneration { get; }
    int AliveCellsCount { get; }
}
