using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image joystickImage;
    [SerializeField] private Image joystickOutlineImage;
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Settings")]
    [SerializeField] private float radius = 1.0f;

    private bool _isJoystickActive;
    
    private Vector2 _touchStart;
    private Vector2 _touchEnd;

    private void Update()
    {
        if (playerInput.actions[ControlKeys.ClickUp].WasPerformedThisFrame())
        {
            _isJoystickActive = false;
            DisableImages();
            ResetTouches();
            return;
        }
        
        var clickDownAction = playerInput.actions[ControlKeys.ClickDown];
        if (clickDownAction.WasPerformedThisFrame())
        {
            _touchStart = playerInput.actions[ControlKeys.ClickPosition].ReadValue<Vector2>();
            if (_touchStart.x >= (Screen.width / 2))
            {
                ResetTouches();
                return;
            }
            _isJoystickActive = true;

            EnableImages();
            joystickImage.transform.position = _touchStart;
            joystickOutlineImage.transform.position = _touchStart;
        }
        else if (_isJoystickActive && clickDownAction.IsPressed())
        {
            _touchEnd = playerInput.actions[ControlKeys.ClickPosition].ReadValue<Vector2>();
            var offset = _touchEnd - _touchStart;
            var direction = Vector2.ClampMagnitude(offset, radius * 50);
            joystickImage.transform.position = direction + _touchStart;
        }
    }

    private void DisableImages()
    {
        joystickImage.enabled = false;
        joystickOutlineImage.enabled = false;
    }

    private void EnableImages()
    {
        joystickImage.enabled = true;
        joystickOutlineImage.enabled = true;
    }

    private void ResetTouches()
    {
        _touchEnd = Vector2.zero;
        _touchStart = Vector2.zero;
    }

    public Vector3 GetDirection()
    {
        if (!_isJoystickActive)
            return Vector3.zero;
        
        var offset = _touchEnd - _touchStart;
        var direction = Vector2.ClampMagnitude(offset, radius);
        return new Vector3(direction.x, 0, direction.y);
    }
}