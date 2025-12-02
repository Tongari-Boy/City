using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameClearUI : MonoBehaviour
{
    public TMP_Text timeText;

    void Start()
    {
        //カーソルを表示して動かせるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        float t = GameManager.lastClearTime;

        //時間フォーマット(分:秒.ミリ秒)
        string formatted = string.Format("{0:00}:{1:00}.{2:000}",
            (int)(t / 60),          // 分
            (int)(t % 60),          // 秒
            (int)((t * 1000) % 1000) // ミリ秒
        );

        timeText.text = "Time:\n" + formatted;
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}