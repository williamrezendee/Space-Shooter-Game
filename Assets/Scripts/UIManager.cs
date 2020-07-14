using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // handle to Text
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _livesImg;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;
    [SerializeField] private Button _mainMenuButton;

    private Scene _scene;
    private bool _isPLayerDeath;

    void Start() // Start is called before the first frame update
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) & _isPLayerDeath == true)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            PlayerDeath();
            StartCoroutine(FlickerTextRoutine());
            _mainMenuButton.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
        }
    }

    IEnumerator FlickerTextRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void PlayerDeath()
    {
        _isPLayerDeath = true;
    }
}
