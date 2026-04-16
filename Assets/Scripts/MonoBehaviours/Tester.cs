using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Tester : MonoBehaviour
{
    void Start()
    {
        ConwaySimulation simulation = new ConwaySimulation(new ConwayRules());

        simulation.SetCell(new Vector2Int(0, 1), true);
        simulation.SetCell(new Vector2Int(0, 0), true);
        simulation.SetCell(new Vector2Int(0, -1), true);

        Debug.Log($"Gen {simulation.CurrentGeneration}: {simulation.AliveCellsCount} cells alive");

        simulation.Tick();

        Debug.Log($"Gen {simulation.CurrentGeneration}: {simulation.AliveCellsCount} cells alive.");

        foreach (var cell in simulation.GetAliveCells())
        {
            Debug.Log($"Alive at: {cell}");
        }
    }
}
