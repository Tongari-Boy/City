using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        // カーソルを表示して動かせるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        SceneManager.LoadScene("Enemy");
        //いつかMainにする↑
    }

    public void GoToTitle()
    {
        Debug.Log("Going to Title Screen");
        SceneManager.LoadScene("Title");
    }
}
