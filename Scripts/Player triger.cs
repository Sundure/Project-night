using UnityEngine;

public class Playertriger : MonoBehaviour
{
    public Transform Player;
    private GameObject _triger;
    private float _distanceToPlayer;
    private Vector3 _directionToZone;

    private void Start()
    {
        _triger = gameObject;
    }
    private void Update()
    {
        _directionToZone = Player.transform.position - _triger.transform.position;
        _distanceToPlayer = _directionToZone.magnitude;

        if (_distanceToPlayer < 10f)
        {
            Debug.Log("1");
        }
    }
}
