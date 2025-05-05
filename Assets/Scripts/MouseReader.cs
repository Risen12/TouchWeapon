using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseReader : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _clickAction;

    public event Action OnClickStarted;
    public event Action OnClickReleased;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _clickAction = _playerInput.actions["Click"];
    }

    private void OnEnable()
    {
        _clickAction.started += HandleClickStart;
        _clickAction.canceled += HandleClickEnd;
    }

    private void OnDisable()
    {
        _clickAction.started -= HandleClickStart;
        _clickAction.canceled -= HandleClickEnd;
    }

    private void HandleClickStart(InputAction.CallbackContext context)
    {
        if(IsPointerOverUI() == false)
            OnClickStarted?.Invoke();
    }

    private void HandleClickEnd(InputAction.CallbackContext context)
    {
        if(IsPointerOverUI() == false)
            OnClickReleased?.Invoke();
    }

    private bool IsPointerOverUI() => UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
}