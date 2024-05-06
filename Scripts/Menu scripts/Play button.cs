using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _loadScreen;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void SceneChanger()
    {
        _loadScreen.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }
}
