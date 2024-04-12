using UnityEngine;
public class CameraScript : MonoBehaviour
{
    [SerializeField] private float sensY;
    [SerializeField] private float sensX;
    [SerializeField] private AudioListener _audio;
    [SerializeField] private AudioClip[] _audioArray = new AudioClip[4];
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform orientation;

    float xRotaion;
    float yRotaion;

    [SerializeField] private Transform cameraPosition;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position = cameraPosition.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotaion += mouseX;
        xRotaion -= mouseY;
        xRotaion = Mathf.Clamp(xRotaion, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotaion, yRotaion, 0);
        orientation.rotation = Quaternion.Euler(0, yRotaion, 0);
    }
    public void AudioOff()
    {
        _audio.enabled = false;
    }
    public void AudioScreamer()
    {
        int randomIndex = Random.Range(0, _audioArray.Length);
        _audioSource.clip = _audioArray[randomIndex];
        _audioSource.Play();
    }
}
