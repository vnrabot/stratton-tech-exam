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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

            float speedScale = _camera.orthographicSize / 10f;
            transform.position += direction * (_panSpeed * speedScale * Time.deltaTime);
        }
    }
}
