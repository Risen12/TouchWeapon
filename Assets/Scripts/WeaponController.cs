using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private MouseReader _mouseReader;

    private void OnEnable()
    {
        _mouseReader.OnClickStarted += OnMouseButtonPressed;
        _mouseReader.OnClickReleased += OnMouseButtonReleased;
    }

    private void OnDisable()
    {
        _mouseReader.OnClickStarted -= OnMouseButtonPressed;
        _mouseReader.OnClickReleased -= OnMouseButtonReleased;
    }

    private void OnMouseButtonPressed()
    {
        Vector3 touchPosition = GetTouchPosition();
        Vector3 direction = touchPosition - _weaponHolder.transform.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _weaponHolder.Rotate(angle);

        _weaponHolder.MouseButtonDown();
    }

    private void OnMouseButtonReleased()
    {
        _weaponHolder.MouseButtonUp();
    }

    private Vector3 GetTouchPosition()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPos = Vector2.zero;

        if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
        }
        else if (Touchscreen.current != null)
        {
            var primaryTouch = Touchscreen.current.primaryTouch;

            if (primaryTouch != null && primaryTouch.IsPressed())
            {
                screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            }
        }

        return mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
    }
}
