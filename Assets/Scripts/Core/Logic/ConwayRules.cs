using UnityEngine;

public class ConwayRules : IGameRules
{
    // Rule 1 & 2: Lives if 2 or 3 neighbors
    //Rule 3: Dies if <2 or >3 neighbors
    public bool ShouldSurvive(int aliveNeighbors)
    {
        return aliveNeighbors == 2 || aliveNeighbors == 3;
    }

    //Rule 4: Dead cell becomes alive if exactly 3 neighbors
    public bool ShouldBeBorn(int aliveNeighbors)
    {
        return aliveNeighbors == 3;
    }
}
