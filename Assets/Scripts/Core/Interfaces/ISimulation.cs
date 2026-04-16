using System.Collections.Generic;
using UnityEngine;

public interface ISimulation
{
    void Tick();
    void SetCell(Vector2Int position, bool isAlive);
    void Clear();
}