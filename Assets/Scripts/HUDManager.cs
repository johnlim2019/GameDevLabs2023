using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
  public CanvasGroup HUD;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI endScoreText;
  public Button restartTopRight;
  public Button restartButton;
  public Button pauseButton;
  public CanvasGroup endgame;
  public GameManager gameManager;
  public GameObject highScoreText;
  public GameObject endHighScoreText;
  public IntVariable topScore;


  void Start()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    restartButton.onClick.AddListener(gameManager.ResetGame);
    restartTopRight.onClick.AddListener(gameManager.ResetGame);
    printTopScore();
  }

  public void StartGame(int score)
  {
    endgame.alpha = 0f;
    ScoreIncrement(score);
  }

  public void GameOver(int val)
  {
    if (val == 0)
    {
      HUD.alpha = 0.0f;
      restartTopRight.interactable = false;
      Color colorInvisible = new Color(255, 255, 255)
      {
        a = 0f
      };
      restartTopRight.GetComponent<Image>().color = colorInvisible;
      pauseButton.GetComponent<Image>().color = colorInvisible;
      endgame.alpha = 1.0f;
      endgame.interactable = true; // enable interaction
      endgame.blocksRaycasts = true; // do not block raycasts
    }
  }

  public void RestartGame()
  {
    // score reset
    scoreText.text = "Score: 0";
    endScoreText.text = "Score: 0";
    // close the endgame canvas
    endgame.alpha = 0f;
    endgame.interactable = false; // disable interaction
    endgame.blocksRaycasts = false; // do not block raycasts
    // Set up HUD
    HUD.alpha = 1.0f;
    restartTopRight.interactable = true;
    Color colorVisible = new Color(255, 255, 255)
    {
      a = 1f
    };
    restartTopRight.GetComponent<Image>().color = colorVisible;
    pauseButton.GetComponent<Image>().color = colorVisible;
  }

  public void ScoreIncrement(int score)
  {
    scoreText.text = "Score: " + score.ToString();
    endScoreText.text = "Score: " + score.ToString();
  }

  public void printTopScore()
  {
    string str = "TOP-" + topScore.previousHighestValue.ToString("D4");
    highScoreText.GetComponent<TextMeshProUGUI>().text = str;
    highScoreText.SetActive(true);
    endHighScoreText.GetComponent<TextMeshProUGUI>().text = str;
    endHighScoreText.SetActive(true);
  }

  public void MainMenu()
  {
    SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
  }




}
