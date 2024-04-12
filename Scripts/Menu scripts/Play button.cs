using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SceneChanger()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
