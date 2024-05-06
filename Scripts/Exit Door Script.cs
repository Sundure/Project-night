using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    [SerializeField] private float _timeToOpen = 720;
    public bool DoorIsOpen = false;
    [SerializeField] private GameObject _timeToLeaveUI;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _StormAudioSource;

    [SerializeField] private MonsterAI _monsterAI;
    [SerializeField] private Temperature _temperature;
    [SerializeField] private PlayerDeath _playerDeath;

    [SerializeField] CameraScript _cameraScript;

    [SerializeField] private GameObject _endOne;
    [SerializeField] private GameObject _doorLight;
    private float _endSceneTime;

    public bool PlayerWinBool;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _doorLight.SetActive(false);
    }
    private void Update()
    {
        _timeToOpen -= Time.deltaTime;
        if (_timeToOpen <= 0)
        {
            DoorIsOpen = true;
            _doorLight.SetActive(true);
            _timeToLeaveUI.SetActive(true);
            _audioSource.enabled = true;
            if (PlayerWinBool == true && _playerDeath.IsDie == false)
            {
                PlayerWin();
            }
        }
        _StormAudioSource.volume = _timeToOpen / 720;
    }
    public void PlayerWin()
    {
        _monsterAI._player.transform.position = Vector3.zero;
        _temperature.Health = 999f;
        _cameraScript.AudioOff();
        _endSceneTime += Time.deltaTime;

        _endOne.SetActive(true);
        if (_endSceneTime > 3)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
