using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float _panSpeed = 15f;
    [SerializeField] private float _zoomSpeed = 10f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 50f;

    private Camera _camera;

    void Start()
    {
        TryGetComponent(out _camera);
    }

    void Update()
    {
        HandlePanning();
    }

    private void HandlePanning()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;

        transform.position += moveDirection * _panSpeed * Time.deltaTime;
    }

    private void HandleZooming()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            // Adjust Zoom size
            _camera.orthographicSize -= scroll * _zoomSpeed;

            // Clamp zoom values to prevent flipping or going out too far.
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minZoom, _maxZoom);
        }
    }
}
