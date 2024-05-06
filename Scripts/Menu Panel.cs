using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    public bool Enabled = false;
    [SerializeField] private CameraScript _cameraScript;
    [SerializeField] private GameObject _pin;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Enabled = !Enabled;
            if (Enabled == true)
            {
                _pin.SetActive(false);
            }
            else
            {
                _pin.SetActive(true);
            }
            _menu.SetActive(Enabled);
            if (Enabled == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _cameraScript.sensX = 150f;
                _cameraScript.sensY = 150f;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _cameraScript.sensX = 0f;
                _cameraScript.sensY = 0f;
            }
        }
    }
    public void Continue()
    {
        _pin.SetActive(true);
        Enabled = false;
        _menu.SetActive(Enabled);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _cameraScript.sensX = 150f;
        _cameraScript.sensY = 150f;
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
