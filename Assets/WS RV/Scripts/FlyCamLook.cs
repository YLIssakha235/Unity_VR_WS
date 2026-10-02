using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Lets the trainer turn the FlyCam's "head" with the mouse.
/// By default you hold the RIGHT mouse button to look around,
/// so the LEFT button stays free for placing markers.
/// </summary>
public class FlyCamLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;
    [Tooltip("If ticked, you must hold the right mouse button to look around.")]
    [SerializeField] private bool holdRightClick = true;
    [SerializeField] private bool invertY = false;
    [SerializeField] private float minPitch = -89f;
    [SerializeField] private float maxPitch = 89f;

    private float _yaw;
    private float _pitch;

    private void OnEnable()
    {
        // Start from the camera's current orientation.
        Vector3 e = transform.eulerAngles;
        _yaw = e.y;
        _pitch = e.x > 180f ? e.x - 360f : e.x;
    }

    private void Update()
    {
        bool looking = !holdRightClick || RightButtonHeld();

        // Hide and lock the cursor only while looking around.
        Cursor.lockState = looking ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !looking;

        if (!looking) return;

        Vector2 delta = MouseDelta() * sensitivity;
        _yaw += delta.x;
        _pitch += invertY ? delta.y : -delta.y;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private bool RightButtonHeld()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null && Mouse.current.rightButton.isPressed;
#else
        return Input.GetMouseButton(1);
#endif
    }

    private Vector2 MouseDelta()
    {
#if ENABLE_INPUT_SYSTEM
        // The new Input System gives pixels: scale down to match the old system.
        return Mouse.current != null ? Mouse.current.delta.ReadValue() * 0.1f : Vector2.zero;
#else
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
#endif
    }
}
