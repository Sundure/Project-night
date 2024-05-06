using UnityEngine;
using UnityEngine.UI;
public class Interact : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Temperature _stats;
    [SerializeField] private Warm _warm;
    [SerializeField] private GameObject _itemIndicator;
    [SerializeField] private FlareScript _flareScript;
    const float _raycastDistantion = 1.5f;
    [SerializeField] private ExitDoorScript _exitDoorScript;
    [SerializeField] private PlayerDeath _playerDeath;

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _sliderObject;
    private float _sliderCount;

    [SerializeField] private Playeritems _playeritems;

    [SerializeField] private KeyPickUp _keyPickUp;

    private void Update()
    {
        _sliderCount -= Time.deltaTime * 5;
        _slider.value = _sliderCount;
        if (_sliderCount <= 0)
        {
            _sliderCount = 0;
            _sliderObject.SetActive(false);
        }

        Ray _ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(_ray, out RaycastHit _itemInfo, _raycastDistantion))
        {
            if (_itemInfo.collider.gameObject.CompareTag("Fuel")
                || _itemInfo.collider.gameObject.CompareTag("Stove")
                & _playeritems.FuelCount > 0 || _itemInfo.collider.gameObject.CompareTag("Flare")
                || _itemInfo.collider.gameObject.CompareTag("Door") & _exitDoorScript.DoorIsOpen == true
                || _itemInfo.collider.gameObject.CompareTag("Garbage")
                || _itemInfo.collider.gameObject.CompareTag("Screwdriver")
                || _itemInfo.collider.gameObject.CompareTag("Vent Bolt") & _playeritems.Screwdriver
                || _itemInfo.collider.gameObject.CompareTag("Key")
                || _itemInfo.collider.gameObject.CompareTag("Store Door"))
            {
                _itemIndicator.SetActive(true);
            }
            if (_itemInfo.collider.gameObject.CompareTag("Untagged"))
            {
                _itemIndicator.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.DrawRay(_ray.origin, _ray.direction * _itemInfo.distance, Color.green);
                Debug.Log(_itemInfo.transform.name);
                switch (_itemInfo.collider.gameObject.tag)
                {
                    case "Fuel":
                        Destroy(_itemInfo.collider.gameObject);
                        _playeritems.FuelCount++;
                        _stats.FuelCountTextUI();
                        _itemIndicator.SetActive(false);
                        break;

                    case "Door":
                        if (_exitDoorScript.DoorIsOpen == true && _playerDeath.IsDie == false)
                        {
                            _exitDoorScript.PlayerWinBool = true;
                        }
                        break;

                    case "Flare":
                        Destroy(_itemInfo.collider.gameObject);
                        _playeritems.FlareCount++;
                        _flareScript.FlareTextCount();
                        _itemIndicator.SetActive(false);
                        break;

                    case "Stove":
                        if (_playeritems.FuelCount > 0)
                        {
                            _warm._stoveTime += 15f;
                            _playeritems.FuelCount--;
                            _stats.FuelCountTextUI();
                            if (_playeritems.FuelCount == 0)
                            {
                                _itemIndicator.SetActive(false);
                            }
                        }
                        break;

                    case "Key":
                        Destroy(_itemInfo.collider.gameObject);
                        _playeritems.Key = true;
                        _keyPickUp.KeyPickUpAudio();
                        break;

                    case "Screwdriver":
                        Destroy(_itemInfo.collider.gameObject);
                        _playeritems.Screwdriver = true;
                        break;
                    case "Store Door":
                        _itemInfo.collider.gameObject.GetComponent<StorageDoorSystem>().DoorCheck();
                        break;
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (_itemInfo.collider.gameObject.CompareTag("Garbage"))
                {
                    _sliderObject.SetActive(true);
                    _sliderCount += Time.deltaTime * 6;
                    if (_sliderCount >= 5)
                    {
                        _itemInfo.collider.gameObject.tag = "Loted Garbage";
                        _sliderCount = 0;
                        int chance;
                        chance = Random.Range(0, 10);
                        if (chance <= 4)
                        {
                            _playeritems.FuelCount++;
                            _stats.FuelCountTextUI();
                            _itemIndicator.SetActive(false);
                        }
                        else if (chance == 5)
                        {
                            _playeritems.FlareCount++;
                            _flareScript.FlareTextCount();
                            _itemIndicator.SetActive(false);
                        }
                        else
                        {
                            _itemIndicator.SetActive(false);
                        }
                    }
                }
                else if (_itemInfo.collider.gameObject.CompareTag("Vent Bolt") && _playeritems.Screwdriver)
                {
                    _sliderObject.SetActive(true);
                    _sliderCount += Time.deltaTime * 8;
                    if (_sliderCount >= 5)
                    {
                        _sliderCount = 0;
                        _itemInfo.collider.gameObject.GetComponent<Ventboltcheck>().VentDestroy();
                    }
                }
                else
                {
                    _sliderCount = 0;
                }
            }
        }
        else
        {
            _itemIndicator.SetActive(false);
        }
    }
}
