using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public GameObject _player;
    [SerializeField] private Transform[] _patrolingPointArray = new Transform[8];
    private NavMeshAgent _agent;
    private Vector3 _magnituda;
    private Vector3 _magnitudaToTarget;
    public float MetersToPlayer;
    private float _metersToTarget;
    [SerializeField] private bool _patroling;

    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;

    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacle;

    public bool _isSee;

    private bool _audioCanPlay = true;
    [SerializeField] private Shotandwalk _seeAnim;
    private float _shoutTime;
    [SerializeField] private Temperature _stats;
    [SerializeField] private CameraScript _cameraScript;

    public AudioSource AudioSourceScreamer;
    [SerializeField] private AudioClip[] _audioArray = new AudioClip[2];
    private bool _canPlayAudio;

    [SerializeField] private AudioSource _audioSourceDeath;
    [SerializeField] private AudioClip[] _audioArrayDeath = new AudioClip[2];
    private bool _canPlay = true;

    [SerializeField] private Playermovment _playerMovet;

    private float _stompTime;
    private bool _canPlayStomp;

    [SerializeField] private ExitDoorScript _exitDoorScript;

    void Start()
    {
        AudioSourceScreamer = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        Patroling();
        _audioSourceDeath = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 vector3 = _player.transform.position;
        _magnituda = vector3 - transform.position;
        MetersToPlayer = _magnituda.magnitude;
        Vector3 _playerTarget = (_player.transform.position - transform.position).normalized;

        Vector3 vector3Target = _target.transform.position;
        _magnitudaToTarget = vector3Target - transform.position;
        _metersToTarget = _magnitudaToTarget.magnitude;

        if (_stompTime <= 0.50f && _isSee == false)
        {
            _stompTime += Time.deltaTime;
            if (_stompTime >= 0.50f)
            {
                _canPlayStomp = true;
                _stompTime = 0f;
                _seeAnim.StompAudio();
            }
        }

        if (Vector3.Angle(transform.forward, _playerTarget) < _viewAngle / 2)
        {
            if (MetersToPlayer <= _viewRadius)
            {
                if (Physics.Raycast(transform.position, _playerTarget, MetersToPlayer, _obstacle) == false)
                {
                    _isSee = true;
                }
            }
        }
        if (_patroling == true)
        {
            _agent.destination = _target.position;
        }
        if (_isSee == true)
        {
            _patroling = false;
            _seeAnim.Shout();
            _shoutTime += Time.deltaTime;
            _target = _player.transform;
            if (MetersToPlayer <= 5f && _audioCanPlay == true)
            {
                _audioCanPlay = false;
                _cameraScript.AudioScreamer();
            }
            _agent.speed = 0f;
            _viewRadius = 30f;
            if (_canPlayStomp == true)
            {
                _canPlayStomp = false;
                _stompTime = 0f;
            }
            if (_shoutTime > 2.18f)
            {
                _stompTime += Time.deltaTime;
                if (_stompTime >= 0.50f)
                {
                    _stompTime = 0f;
                    _seeAnim.StompAudio();
                }

                if (_exitDoorScript.DoorIsOpen == true)
                {
                    _agent.speed = 7f;
                }
                else
                {
                _agent.speed = 5f;
                }
                _agent.destination = _target.position;
                if (_shoutTime > 7f && MetersToPlayer >= 15f)
                {
                    _isSee = false;
                    Patroling();
                }
                if (MetersToPlayer <= 1f)
                {
                    _stats.IsDie = true;
                    _stats.PlayerDeath();
                }
            }
        }
        else if (MetersToPlayer <= 1f)
        {
            _isSee = true;
            _agent.destination = _target.position;
        }

        if (_metersToTarget <= 1f)
        {
            Patroling();
        }
        if (MetersToPlayer > _viewRadius && _patroling == false)
        {
            _isSee = false;
            Patroling();
        }
        if (MetersToPlayer <= 5f && _playerMovet._crouch == false)
        {
            _isSee = true;
        }
    }
    private void Patroling()
    {
        if (_isSee == false)
        {
            _viewRadius = 13f;
            _canPlayAudio = true;

            if (_exitDoorScript.DoorIsOpen == true)
            {
                _agent.speed = 6f;
            }
            else
            {
            _agent.speed = 4f;
            }
            _audioCanPlay = true;
            _shoutTime = 0f;
            int _randomIndex = Random.Range(0, _patrolingPointArray.Length);
            _target = _patrolingPointArray[_randomIndex];
            _patroling = true;
        }
    }
    public void SpiderAudioScreamer()
    {
        if (_canPlayAudio == true)
        {
            _canPlayAudio = false;
            int randomIndex = Random.Range(0, _audioArray.Length);

            AudioSourceScreamer.clip = _audioArray[randomIndex];
            AudioSourceScreamer.Play();
        }

    }
    public void DeathAudioScreamer()
    {
        if (_canPlay == true)
        {
            _canPlay = false;
            int randomIndex = Random.Range(0, _audioArrayDeath.Length);
            _audioSourceDeath.clip = _audioArrayDeath[randomIndex];
            _audioSourceDeath.Play();
        }
    }
}