using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Sway Setings")]
    [SerializeField] private float _Smooth;
    [SerializeField] private float _Multiplier;
    void Update()
    {
        if (Cursor.visible)
        {
            return;
        }
        float _Mouse_X = Input.GetAxisRaw("Mouse X") * _Multiplier;
        float _Mouse_Y = Input.GetAxisRaw("Mouse Y") * _Multiplier;

        Quaternion _Rotation_X = Quaternion.AngleAxis(_Mouse_X, Vector3.up);
        Quaternion _Rotation_Y = Quaternion.AngleAxis(-_Mouse_Y, Vector3.right);

        Quaternion _Target_Rotation = _Rotation_X * _Rotation_Y;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, _Target_Rotation, _Smooth * Time.deltaTime);
    }

}
