using UnityEngine;
using UnityEngine.UI;

public class BlackScreenEffect : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;
    [SerializeField] private GameObject _blackScreenObject;
    private float _turnOffScript = 3;
    private void Update()
    {
        Color curentlyColor = _blackScreen.color;
        curentlyColor.a -= Time.deltaTime;
        _blackScreen.color = curentlyColor;
        _turnOffScript -= Time.deltaTime;
        if (_turnOffScript <= 0)
        {
            Destroy(gameObject);
        }
    }
}
