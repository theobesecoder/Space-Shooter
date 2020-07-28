using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Sprite[] sprite;
    [SerializeField]
    private Image livesImage;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;

    private GameManager gameManager;
    // Start is called before the first frame update

    void Start()
    {
        _score.text = "Score : " + 0;
        _gameOver.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerScore)
    {
        _score.text = "Score :" + playerScore.ToString();
    }

    public void UpdateImage(int currentLives)
    {
        livesImage.sprite = sprite[currentLives];
    }

    public void GameOverText()
    {
        _gameOver.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        gameManager.GameOver();


        StartCoroutine(flickerText());      

    }

    private IEnumerator flickerText()
    {
        while (true)
        {
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
