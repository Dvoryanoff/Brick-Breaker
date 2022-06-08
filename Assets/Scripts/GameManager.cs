using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    
    public Brick[] bricks { get; private set; }
    
    public int score = 0;
    public int level = 1;
    public int lives = 3;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;
        
        LoadLevel(1);
    }


    private void LoadLevel(int level)
    {
        this.level = level;
        
        // if (level > 10)
        // {
        //     SceneManager.LoadScene("WinScreen");
        // }
        // else
        // {
        //     SceneManager.LoadScene("Level" + level);
        // }
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();

    }

    public void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();

        // for (int i = 0; i < this.bricks.Length;i++)
        // {
        //     this.bricks[i].ResetBrick();
        // }
    }

    public void GameOver()
    {
        // SceneManager.LoadScene("GameOver");
        NewGame();
    }

    public void Miss()
    {
        this.lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }
    public void Hit(Brick brick)
    {
        this.score += brick.points;

        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }    

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreackable)
            {
                return false;
            }
        }

        return true;
    }
    
}
