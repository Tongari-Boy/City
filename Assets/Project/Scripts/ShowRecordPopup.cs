using UnityEngine;
using TMPro;

public class ShowRecordPopup : MonoBehaviour
{
    public GameObject RecordPopup;

    public Transform contentParent;
    public GameObject recordItemPrefab;

    private DataBase db;

    //データベース初期化
    void Awake()
    {
        db = new DataBase();
    }

    //ポップアップが有効化されたときに記録を読み込む
    void OnEnable()
    {
        LoadRecords();
    }

    //ポップアップが表示されたときに記録を読み込む
    void LoadRecords()
    {
        //内容クリア
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        //データベースから記録を取得
        var records = db.connection.Query<ClearTimeRecord>(
            "SELECT * FROM ClearTimeRecord ORDER BY time ASC Limit 15"
        );

        foreach (var record in records)
        {
            var item = Instantiate(recordItemPrefab, contentParent);
            
            TMP_Text text = item.GetComponentInChildren<TMP_Text>();

            text.text = FormatTime(record.time);
        }
    }

    //時間フォーマット(分:秒.ミリ秒)
    string FormatTime(float t)
    {
        return string.Format("{0:00}:{1:00}.{2:000}",
            (int)(t / 60),
            (int)(t % 60),
            (int)((t * 1000) % 1000)
        );
    }

    //閉じるボタン
    public void OnClickCloseButton()
    {
        RecordPopup.SetActive(false);
    }
}