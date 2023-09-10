using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthNav : MonoBehaviour
{
    public GameObject registerScreen;
    public GameObject authScreen;

    public void RegisterScreenLoad()
    {
        registerScreen.SetActive(true);
        authScreen.SetActive(false);
    }

    public void MoveToNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
