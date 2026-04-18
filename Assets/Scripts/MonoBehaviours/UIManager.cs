using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle _playPauseToggle;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Slider _zoomSlider;
    [SerializeField] private Slider _cellSizeSlider;
    [SerializeField] private Button _clearButton;
    [SerializeField] private TMP_Dropdown _colorDropdown;
    [SerializeField] private TextMeshProUGUI _generationText;
    [SerializeField] private TextMeshProUGUI _aliveCountText;
    [Header("System References")]
    [SerializeField] private SimulationController _simulationController;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GridRenderer _gridRenderer;

    void Start()
    {
        // Speed Slider starting settings
        _speedSlider.minValue = 1f;
        _speedSlider.maxValue = 30f;
        _speedSlider.value = 5f;


        // Zoom Slider settings
        _zoomSlider.minValue = 5f;
        _zoomSlider.maxValue = 50f;
        float invertedStartZoom = (_zoomSlider.maxValue + _zoomSlider.minValue) - _mainCamera.orthographicSize;
        _zoomSlider.value = invertedStartZoom;

        //Cell Size Slider
        _cellSizeSlider.minValue = 0.2f;
        _cellSizeSlider.maxValue = 3.0f;
        _cellSizeSlider.value = 1.0f;

        // Event Listeners
        _playPauseToggle.onValueChanged.AddListener(isOn => _simulationController.SetPlaying(isOn));
        _speedSlider.onValueChanged.AddListener(value => _simulationController.SetSpeed(value));
        _zoomSlider.onValueChanged.AddListener(value =>
        {
            float invertedZoom = (_zoomSlider.maxValue + _zoomSlider.minValue) - value;
            _mainCamera.orthographicSize = invertedZoom;
        });
        _cellSizeSlider.onValueChanged.AddListener(value => _simulationController.SetCellSize(value));
        _clearButton.onClick.AddListener(() => _simulationController.ClearBoard());
        _colorDropdown.onValueChanged.AddListener(ChangeColor);


        _simulationController.SetPlaying(_playPauseToggle.isOn);
        _simulationController.SetSpeed(_speedSlider.value);
        _simulationController.SetCellSize(_cellSizeSlider.value);

        ChangeColor(_colorDropdown.value);

        _simulationController.OnStatsUpdated.AddListener(RefreshDataDisplay);

        RefreshDataDisplay(_simulationController.GetGeneration(), _simulationController.GetAliveCount());
    }

    void OnDestroy()
    {
        // Remove Events to prevent memory leaks
        if (_simulationController != null)
        {
            _simulationController.OnStatsUpdated.RemoveListener(RefreshDataDisplay);
        }

        _playPauseToggle.onValueChanged.RemoveAllListeners();
        _speedSlider.onValueChanged.RemoveAllListeners();
        _zoomSlider.onValueChanged.RemoveAllListeners();
        _cellSizeSlider.onValueChanged.RemoveAllListeners();
        _clearButton.onClick.RemoveAllListeners();
        _colorDropdown.onValueChanged.RemoveAllListeners();
    }

    public void ChangeColor(int index)
    {
        Color selectedColor = index switch
        {
            0 => Color.red,
            1 => Color.white,
            2 => Color.blue,
            3 => Color.yellow,
            4 => Color.green,
            5 => Color.cyan,
            _ => Color.red
        };

        Debug.Log($"Color changed to {selectedColor}");

        _gridRenderer.SetTileColor(selectedColor);
    }

    private void RefreshDataDisplay(int generation, int aliveCount)
    {
        _generationText.text = $"GENERATION: {generation}";
        _aliveCountText.text = $"ALIVE:  {aliveCount}";
    }
}
