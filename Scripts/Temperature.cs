using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Temperature : MonoBehaviour
{
    public float Health;
    public bool IsDie;
    public int FuelCount;
    [SerializeField] private GameObject _deathImage;
    [SerializeField] private Text _fuelCountText;
    [SerializeField] private CameraScript _cam;
    [SerializeField] private MonsterAI _monsterAI;
    [SerializeField] private FrostEffect _frostEffect;
    private float _audioTime;
    private void Update()
    {
        _frostEffect.FrostAmount = Health / 100;
        if (IsDie == true)
        {
            _audioTime += Time.deltaTime;
        }
    }
    public void PlayerDeath()
    {
        if (IsDie == true)
        {
            if (_monsterAI.MetersToPlayer <= 1f)
            {
                _monsterAI.DeathAudioScreamer();
            }
            _deathImage.SetActive(true);
            if (_audioTime > 3.5f)
            {
                _cam.AudioOff();

                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Menu");

            }
        }
    }
    public void FuelCountTextUI()
    {
        _fuelCountText.text = FuelCount.ToString();
    }

}