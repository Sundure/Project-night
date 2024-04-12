using UnityEngine;
public class Fuelscript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Temperature _stats;
    [SerializeField] private Warm _warm;
    [SerializeField] private GameObject _itemIndicator;
    [SerializeField] private FlareScript _flareScript;
    const float _raycastDistantion = 1.5f;
    [SerializeField] private ExitDoorScript _exitDoorScript;

    private void Update()
    {
        Ray _ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(_ray, out RaycastHit _itemInfo, _raycastDistantion))
        {
            if (_itemInfo.collider.gameObject.CompareTag("Fuel") | _itemInfo.collider.gameObject.CompareTag("Stove") & _stats.FuelCount > 0 | _itemInfo.collider.gameObject.CompareTag("Flare") | _itemInfo.collider.gameObject.CompareTag("Door") & _exitDoorScript.DoorIsOpen == true)
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

                if (_itemInfo.collider.gameObject.CompareTag("Fuel"))
                {
                    Destroy(_itemInfo.collider.gameObject);
                    _stats.FuelCount++;
                    _stats.FuelCountTextUI();
                }
                if (_itemInfo.collider.gameObject.CompareTag("Door") && _exitDoorScript.DoorIsOpen == true && _stats.IsDie == false)
                {
                    _exitDoorScript.PlayerWinBool = true;
                }
                if (_itemInfo.collider.gameObject.CompareTag("Flare"))
                {
                    Destroy(_itemInfo.collider.gameObject);
                    _flareScript.CountOfFlare++;
                    _flareScript.FlareTextCount();
                }
                if (_itemInfo.collider.gameObject.CompareTag("Stove") && _stats.FuelCount > 0)
                {
                    _warm._stoveTime += 30f;
                    _stats.FuelCount--;
                    _stats.FuelCountTextUI();
                }
            }
        }
        else
        {
            _itemIndicator.SetActive(false);
        }
    }
}
