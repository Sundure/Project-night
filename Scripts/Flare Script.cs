using UnityEngine;
using UnityEngine.UI;

public class FlareScript : MonoBehaviour
{
    [SerializeField] private GameObject _flareLightSystem;
    [SerializeField] private GameObject _flareObject;
    [SerializeField] private MeshRenderer _flareCap;

    [SerializeField] private float _flareIntensity;
    [SerializeField] private float _flareBurnTime = 140;
    [SerializeField] private Light _flareLight;

    [SerializeField] private bool _burn;
    public float FlareCharge;
    

    [SerializeField] private Slider _flareSlider;
    [SerializeField] private GameObject _objectSlider;

    [SerializeField] private Text _flareTextCount;

    [SerializeField] private MenuPanel _menuPanel;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private Playeritems _playeritems;

    private void Update()
    {
        FlareCharge -= Time.deltaTime;
        _flareSlider.value = FlareCharge;
        if (FlareCharge < 0)
        {
            _objectSlider.SetActive(false);
            FlareCharge = 0;
        }
        if (Input.GetKey(KeyCode.Mouse0) && _playeritems.FlareCount > 0 && _menuPanel.Enabled == false)
        {
            _objectSlider.SetActive(true);
            FlareCharge += Time.deltaTime * 2;
            if (FlareCharge >= 1)
            {
                _flareBurnTime = 140;
                _playeritems.FlareCount--;
                FlareTextCount();
                _objectSlider.SetActive(false);
                FlareCharge = 0;
                _burn = true;
                _flareCap.enabled = false;
                _flareLightSystem.SetActive(true);
            }
        }
        if (_flareBurnTime <= 0)
        {
            _burn = false;
            _flareCap.enabled = true;
            _flareObject.SetActive(false);
        }
        if (_burn == true)
        {
            _flareBurnTime -= Time.deltaTime;
            _flareIntensity = _flareBurnTime;
            _flareLight.range = _flareIntensity / 7;
            _flareLight.intensity = _flareIntensity / 80;
            _audioSource.volume = _flareIntensity / 520;
            var _particleSystemSpeed = _particleSystem.main;
            _particleSystemSpeed.simulationSpeed = (_flareIntensity + 10) / 28; 
        }
        if (_burn == false && _playeritems.FlareCount > 0)
        {
            _flareCap.enabled = true;
            _flareLightSystem.SetActive(false);
            _flareObject.SetActive(true);
        }
    }
    public void FlareTextCount()
    {
        _flareTextCount.text = _playeritems.FlareCount.ToString();
    }
}
