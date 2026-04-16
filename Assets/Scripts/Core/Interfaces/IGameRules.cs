using System.Collections.Generic;
using UnityEngine;


//Single Responsibility of the game's rules
public interface IGameRules
{
    bool ShouldSurvive(int aliveNeighbors);
    bool ShouldBeBorn(int aliveNeighbors);
}
