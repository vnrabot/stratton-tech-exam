using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class SimulationController : MonoBehaviour
{
    [Header("Local Dependencies")]
    [SerializeField] private GridRenderer _gridRenderer;

    private ISimulation _simulation;

    void Start()
    {
        _simulation = new ConwaySimulation(new ConwayRules());

        _gridRenderer.Setup((IGameState)_simulation);

        // Test Setup
        _simulation.SetCell(new Vector2Int(0, 0), true);
        _simulation.SetCell(new Vector2Int(1, 0), true);
        _simulation.SetCell(new Vector2Int(2, 0), true);
        _simulation.SetCell(new Vector2Int(2, 1), true);
        _simulation.SetCell(new Vector2Int(1, 2), true);

        _gridRenderer.UpdateVisuals();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _simulation.Tick();
            _gridRenderer.UpdateVisuals();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Vector3 worldPosition _mainCam
        }
    }
}
