using UnityEngine;
using UnityEngine.UI;
public class Temperature : MonoBehaviour
{
    public float Health;
    [SerializeField] private Playeritems _playeritems;

    [SerializeField] private Text _fuelCountText;
    [SerializeField] private FrostEffect _frostEffect;

    private void Update()
    {
        _frostEffect.FrostAmount = Health / 100;
    }
    public void FuelCountTextUI()
    {
        _fuelCountText.text = _playeritems.FuelCount.ToString();
    }
}