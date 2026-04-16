using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ConwaySimulation : ISimulation, IGameState
{
    private HashSet<Vector2Int> _aliveCells;
    private readonly IGameRules _rules;

    public int CurrentGeneration { get; private set; }
    public int AliveCellsCount => _aliveCells.Count;

    public ConwaySimulation(IGameRules rules)
    {
        _rules = rules;
        _aliveCells = new HashSet<Vector2Int>();
        CurrentGeneration = 0;
    }

    public IEnumerable<Vector2Int> GetAliveCells() => _aliveCells;

    public void SetCell(Vector2Int position, bool isAlive)
    {
        if (isAlive) _aliveCells.Add(position);
        else _aliveCells.Remove(position);
    }

    public void Clear()
    {
        _aliveCells.Clear();
        CurrentGeneration = 0;
    }

    public void Tick()
    {
        HashSet<Vector2Int> nextGeneration = new HashSet<Vector2Int>();
        HashSet<Vector2Int> cellsToEvaluate = new HashSet<Vector2Int>();

        //Gather alive cells and their dead neighbors
        foreach (var cell in _aliveCells)
        {
            cellsToEvaluate.Add(cell);
            foreach (var neighbor in GetNeighbors(cell))
            {
                cellsToEvaluate.Add(neighbor);
            }
        }

        //Evaluate rules for all gathered cells
        foreach (var cell in cellsToEvaluate)
        {
            int aliveNeighbors = CountAliveNeighbors(cell);
            bool isCurrentlyAlive = _aliveCells.Contains(cell);

            if (isCurrentlyAlive && _rules.ShouldSurvive(aliveNeighbors))
            {
                nextGeneration.Add(cell);
            }
            else if (!isCurrentlyAlive && _rules.ShouldBeBorn(aliveNeighbors))
            {
                nextGeneration.Add(cell);
            }
        }

        //Swap generation and increment count.
        _aliveCells = nextGeneration;
        CurrentGeneration++;
    }

    private int CountAliveNeighbors(Vector2Int cell)
    {
        int count = 0;
        foreach (var neighbor in GetNeighbors(cell))
        {
            if (_aliveCells.Contains(neighbor)) count++;
        }
        return count;
    }

    private IEnumerable<Vector2Int> GetNeighbors(Vector2Int cell)
    {
        yield return new Vector2Int(cell.x - 1, cell.y + 1); // Top left
        yield return new Vector2Int(cell.x, cell.y + 1); //directly Top
        yield return new Vector2Int(cell.x + 1, cell.y + 1); //Top Right
        yield return new Vector2Int(cell.x - 1, cell.y); //Left
        yield return new Vector2Int(cell.x + 1, cell.y); //Right
        yield return new Vector2Int(cell.x - 1, cell.y - 1); //Bottom Left
        yield return new Vector2Int(cell.x, cell.y - 1); //directly Bottom
        yield return new Vector2Int(cell.x + 1, cell.y - 1); // Bottom Right
    }
}
