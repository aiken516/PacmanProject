using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] public Field Field;
    [SerializeField] public UIManager UIManager;

    [SerializeField] private GameObject _gameOverScroll;

    public int Score => _score;
    private int _score = 0;

    public bool IsPlay = true;
    
    public void AddScore(int score)
    {
        _score += score;
        UIManager.SetScore(_score);
    }

    public void GameOver()
    {
        UIManager.SetGameOverUI();
        IsPlay = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().SetTrigger("Die");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSound>().DeadSFX.Play();
        _gameOverScroll.SetActive(false);
    }

    public void OnRetryButtonClick()
    {
        UIManager.SetFadeOut();
        PlayAfterCoroutine(() => {
            SceneManager.LoadScene("GameScene");
        }, 3f);
    }

    public void OnMenuButtonClick()
    {
        UIManager.SetFadeOut();
        PlayAfterCoroutine(() => {
            SceneManager.LoadScene("StartScene");
        }, 3f);
    }

    public void PlayAfterCoroutine(Action action, float time) => StartCoroutine(PlayCoroutine(action, time));
    private IEnumerator PlayCoroutine(Action action, float time)
    {
        yield return new WaitForSeconds(time);

        action();
    }
}
