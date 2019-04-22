using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.Scripts.Constants;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    #region Components

    public GameObject gameOverText;
    public bool GameOver = false;
    public float ScrollSpeed = -1.5f;
    public Text ScoreText;

    #endregion Components

    private int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver && Input.GetKeyUp(KeyNames.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        GameOver = true;
    }


    public void Scored()
    {
        if (GameOver) return;
        score++;
        ScoreText.text = $"Score: {score}";
    }
}
