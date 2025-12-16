using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        //カーソルを表示して動かせるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("City");
        //いつかMainにする↑
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
