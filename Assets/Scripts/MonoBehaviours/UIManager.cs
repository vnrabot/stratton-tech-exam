using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle _playPauseToggle;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Slider _zoomSlider;
    [SerializeField] private Button _clearButton;
    [SerializeField] private TMP_Dropdown _colorDropdown;

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
        _zoomSlider.value = _mainCamera.orthographicSize;

        // Event Listeners
        _playPauseToggle.onValueChanged.AddListener(isOn => _simulationController.SetPlaying(isOn));
        _speedSlider.onValueChanged.AddListener(value => _simulationController.SetSpeed(value));
        _zoomSlider.onValueChanged.AddListener(value => _mainCamera.orthographicSize = value);
        _clearButton.onClick.AddListener(() => _simulationController.ClearBoard());
        _colorDropdown.onValueChanged.AddListener(ChangeColor);


        _simulationController.SetPlaying(_playPauseToggle.isOn);
        _simulationController.SetSpeed(_speedSlider.value);
        _mainCamera.orthographicSize = _zoomSlider.value;

        ChangeColor(_colorDropdown.value);
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

        Debug.Log($"COlor changed to {selectedColor}");

        _gridRenderer.SetTileColor(selectedColor);
    }
}
