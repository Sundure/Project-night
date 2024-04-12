using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FlareScript : MonoBehaviour
{
    [SerializeField] private GameObject _flareLightSystem;
    [SerializeField] private GameObject _flareObject;
    [SerializeField] private MeshRenderer _flareCap;

    [SerializeField] private float _flareIntensity;
    [SerializeField] private float _flareBurnTime = 160f;
    [SerializeField] private Light _flareLight;

    private bool _burn;
    private float _flareCharge;
    public int CountOfFlare;

    [SerializeField] private Slider _flareSlider;
    [SerializeField] private GameObject _objectSlider;

    [SerializeField] private Text _flareTextCount;

    private void Update()
    {
        _flareCharge -= Time.deltaTime;
        _flareSlider.value = _flareCharge;
        if (_flareCharge < 0)
        {
            _objectSlider.SetActive(false);
            _flareCharge = 0;
        }
        if (Input.GetKey(KeyCode.Mouse0) && _burn == false && CountOfFlare > 0)
        {
            _objectSlider.SetActive(true);
            _flareCharge += Time.deltaTime * 2;
            if (_flareCharge >= 1)
            {
                CountOfFlare--;
                FlareTextCount();
                _objectSlider.SetActive(false);
                _flareCharge = 0;
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
        }
        if (_burn == false && CountOfFlare > 0)
        {
            _flareBurnTime = 160f;
            _flareCap.enabled = true;
            _flareLightSystem.SetActive(false);
            _flareObject.SetActive(true);
        }
    }
    public void FlareTextCount()
    {
        _flareTextCount.text = CountOfFlare.ToString();
    }
}
