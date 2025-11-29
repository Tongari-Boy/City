using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerStatus player;

    private float startTime;
    private bool isStarted = false;

    private DataBase db;

    //クリア後にみせるTime
    public static float lastClearTime;

    void Start()
    {
        db = new DataBase();

        //ゲーム開始時に時間計測スタート
        startTime = Time.time;
        isStarted = true;
    }

    void Update()
    {
        //ゲームオーバ
        if (player.currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameClear()
    {
        if (!isStarted) return;
        float clearTime = Time.time - startTime;

        db.InsertClearTime(clearTime);

        lastClearTime = clearTime;

        SceneManager.LoadScene("GameClear");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");    
    }
}