using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class SimulationController : MonoBehaviour
{
    [Header("Local Dependencies")]
    [SerializeField] private GridRenderer _gridRenderer;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Tilemap _tilemap;

    private ISimulation _simulation;

    // for UIManager functionalities
    private bool _isPlaying = false;
    private float _tickTimer = 0f;
    private float _tickRate = 5f;

    public int GetGeneration() => ((IGameState)_simulation).CurrentGeneration;
    public int GetAliveCount() => ((IGameState)_simulation).AliveCellsCount;

    public UnityEvent<int, int> OnStatsUpdated = new UnityEvent<int, int>();

    void Awake()
    {
        _simulation = new ConwaySimulation(new ConwayRules());
        _gridRenderer.Setup((IGameState)_simulation);

        // Test Setup
        _simulation.SetCell(new Vector2Int(0, 0), true);
        _simulation.SetCell(new Vector2Int(1, 0), true);
        _simulation.SetCell(new Vector2Int(2, 0), true);
        _simulation.SetCell(new Vector2Int(2, 1), true);
        _simulation.SetCell(new Vector2Int(1, 2), true);

        UpdateVisualsAndBroadcast();
    }

    void Update()
    {
        HandleMouseInput();

        if (_isPlaying)
        {
            // Tick-based speed slider
            _tickTimer += Time.deltaTime;
            if (_tickTimer >= (1f / _tickRate))
            {
                _tickTimer = 0f;
                _simulation.Tick();
                UpdateVisualsAndBroadcast();
            }
        }

        // manual Spacebar input for debugging
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _simulation.Tick();
            UpdateVisualsAndBroadcast();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse pixel position and convert to 3D world position
            UnityEngine.Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Snapshat raw 3D position to Grid integer
            Vector3Int cellPosition = _tilemap.WorldToCell(worldPosition);

            //Convert Unity's 3D coordinate to 2d coordinate
            Vector2Int logicPosition = new Vector2Int(cellPosition.x, cellPosition.y);

            //Inject new alive cell
            if (_simulation != null)
                _simulation.SetCell(logicPosition, true);

            UpdateVisualsAndBroadcast();
        }
    }


    #region UI FUNCTIONALITIES
    public void SetPlaying(bool isPlaying) => _isPlaying = isPlaying;
    public void SetSpeed(float speed) => _tickRate = speed;

    public void ClearBoard()
    {
        ((ConwaySimulation)_simulation).Clear();
        UpdateVisualsAndBroadcast();
    }

    public void SetCellSize(float size)
    {
        _gridRenderer.SetCellScale(size);
    }

    private void UpdateVisualsAndBroadcast()
    {
        if (_gridRenderer != null)
            _gridRenderer.UpdateVisuals();

        OnStatsUpdated?.Invoke(GetGeneration(), GetAliveCount());
    }

    #endregion
}
