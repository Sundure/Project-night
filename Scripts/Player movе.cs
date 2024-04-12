using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Playermovment : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _playerHeight;
    private float _groundDrag;
    [SerializeField] private LayerMask _whatIsGround;
    private bool grounded;
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _scale;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private Rigidbody _rb;
    Vector3 _moveDirection;
    const float _sprintEventChance = 0.0015f;
    private bool _canRun = true;
    public bool _crouch;
    public bool Fall;
    private float _decarseSpeed = 1f;
    [SerializeField] private bool _canStay;

    [SerializeField] private AnimScripts _anim;

    private void Start()
    {
        _anim.FallAnimator.enabled = false;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }
    private void Update()
    {
        SpeedControl();
        grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

        MyInput();

        if (grounded)
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && _canRun == true && _crouch == false)
        {
            Sprint();
        }
        else if (_canRun == true && _crouch == false && _canStay == true)
        {
            _anim.FallAnimator.enabled = false;
            _moveSpeed = 3f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && _canRun)
        {
            RaycastHit hit;
            float raycastDistance = 1.5f;
            Vector3 raycastDirection = Vector3.up;

            if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance) && _crouch == true)
            {
                _canStay = false;
            }
            else
            {
                _canStay = true;
            }

            if (_canStay == true)
            {
                _crouch = !_crouch;
            }
        }
        if (_crouch == true)
        {
            _moveSpeed = 1.5f;
            Vector3 _currentScale = transform.localScale;

            _currentScale.y -= _decarseSpeed * Time.deltaTime;

            _currentScale.y = Mathf.Max(_currentScale.y, 0.5f);
            transform.localScale = _currentScale;
        }
        else if (_canStay == true)
        {
            Vector3 _currentScale = transform.localScale;

            _currentScale.y += _decarseSpeed * Time.deltaTime;

            _currentScale.y = Mathf.Clamp(_currentScale.y, 0.5f, 1f);
            transform.localScale = _currentScale;
        }
        PlayerMove();
    }
    private void FixedUpdate()
    {
    }
    private async void Sprint()
    {
        _moveSpeed = 7f;
        if (Random.value <= _sprintEventChance)
        {
            _anim.FallAnimator.enabled = true;
            Debug.Log("Player Fall");
            _canRun = false;
            _moveSpeed = 0f;
            Fall = true;
            _anim.FallAnim();
            await Task.Delay(1800);
            _anim.FallAnim();
            Fall = false;
            _canRun = true;
        }
    }
    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void PlayerMove()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        _rb.AddForce(10f * _moveSpeed * _moveDirection.normalized, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }
}
