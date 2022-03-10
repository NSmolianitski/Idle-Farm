using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private GameObject weapon;
    [SerializeField] private VirtualJoystick joystick;
    
    private Vector3 _direction;
    private Rigidbody _rb;
    private Animator _animator;
    private Collider _weaponCollider;
    private bool _isAttacking;
    
    private static readonly int Velocity = Animator.StringToHash("Velocity");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _weaponCollider = weapon.GetComponent<Collider>();

        _weaponCollider.enabled = false;
        weapon.SetActive(false);
    }

    private void Update()
    {
        _direction = joystick.GetDirection();
    }

    private void FixedUpdate()
    {
        if (!_isAttacking)
            Move(_direction);
        
        _animator.SetFloat(Velocity, _rb.velocity.magnitude);
    }

    private void Move(Vector3 direction)
    {
        _rb.velocity = direction * (Time.deltaTime * settings.Speed);
        if (direction.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Hit()
    {
        _isAttacking = true;
        _rb.velocity = Vector3.zero;
        weapon.SetActive(true);
        _animator.SetTrigger(Attack);
    }

    public void EnableWeaponCollider()
    {
        _weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        _weaponCollider.enabled = false;
    }
    
    public void ActionAfterAttack()
    {
        DisableWeaponCollider();
        weapon.SetActive(false);
        _isAttacking = false;
    }
}