using UnityEngine;

public class Warm : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _triger;
    [SerializeField] private GameObject _stoveLight;
    [SerializeField] private Temperature _stats;
    private float _distanceToPlayer;
    private Vector3 _directionToZone;
    public float _stoveTime;

    private float _maxVolume = 1f;
    private float _maxDistance = 7f;
    private float _volume;

    [SerializeField] private AudioSource _audioSource;
    private void Start()
    {
        _triger = gameObject;
    }
    private void Update()
    {
        _directionToZone = _player.transform.position - _triger.transform.position;
        _distanceToPlayer = _directionToZone.magnitude;
        _stoveTime -= Time.deltaTime;
        if (_stoveTime < 0)
        {
            _stoveLight.SetActive(false);
            _stoveTime = 0;
        }
        if (_stoveTime > 0 )
        {
            _stoveLight.SetActive(true);
            TemperatureSoundEffect();
            if (_distanceToPlayer < 5f)
            {
                IsWarm();
            }
        }
        if ( _distanceToPlayer >= 5f | _stoveTime == 0 ) 
            NotWarm();
    }
    private void IsWarm()
    {
        _stats.Health += Time.deltaTime * 0.30f;
        if (_stats.Health >= 100)
        {
            _stats.Health = 100;
        }
    }
    private void NotWarm()
    {
        _stats.Health -= Time.deltaTime * 0.20f;
        if (_stats.Health <= 0)
        {
            _stats.IsDie = true;
            _stats.PlayerDeath();
        }
    }
    private void TemperatureSoundEffect()
    {
        _volume = Mathf.Clamp01(1 - _distanceToPlayer / _maxDistance) * _maxVolume;
        _audioSource.volume = _volume;
    }
}