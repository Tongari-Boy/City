using UnityEngine;

public class ClearTimeManager : MonoBehaviour
{
    private DataBase db;
    private float startTime;
    private bool isStarted = false;

    void Start()
    {
        db = new DataBase();
    }

    //スタートボタン
    public void OnStart()
    {
        startTime = Time.time;
        isStarted = true;
    }

    //クリア時に保存
    public void OnClear()
    {
        if (!isStarted) return;

        float clearTime = Time.time - startTime;

        //保存
        db.InsertClearTime(clearTime);

        Debug.Log("Clear Time Saved: " + clearTime);

        isStarted = false;
    }

    //履歴一覧を表示
    public void ShowAll()
    {
        foreach (var r in db.GetAll())
        {
            Debug.Log($"ID:{r.id}  Time:{r.time}  At:{r.created_at}");
        }
    }
}
