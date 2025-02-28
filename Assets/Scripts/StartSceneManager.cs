using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Image _fade;

    public void OnStartButtonClick()
    {
        _fade.GetComponent<Animator>().Play("Fade");
        Invoke(nameof(ChangeSceneToMain), 3f);
    }

    public void ChangeSceneToMain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
