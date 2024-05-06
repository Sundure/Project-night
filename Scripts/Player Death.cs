using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    public bool IsDie;
    [SerializeField] private GameObject _deathMovie;

    [SerializeField] private MonsterAI _monsterAI;
    public async void PlayerDeathMethod()
    {
        if (IsDie == true)
        {
            _deathMovie.SetActive(true);
            _monsterAI._player.transform.position = Vector3.zero;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            await Task.Delay(2900);
            SceneManager.LoadScene("Menu");
        }
    }
}
