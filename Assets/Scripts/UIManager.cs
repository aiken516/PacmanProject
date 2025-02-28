using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private Image _fadeOut;

    public void SetScore(int score)
    {
        _scoreText.text = $"{score}";
    }

    public void SetGameOverUI() => _gameOverUI.SetActive(true);

    public void SetFadeOut()
    {
        _fadeOut.GetComponent<Animator>().Play("Fade");
    }
}
