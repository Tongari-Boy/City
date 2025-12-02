using UnityEngine;
using SQLite4Unity3d;

public class SQLiteTest : MonoBehaviour
{

    void Start()
    {
        // DBファイルの保存場所
        string path = System.IO.Path.Combine(Application.persistentDataPath, "test.db");

        // DBに接続（無ければ新規作成）
        var db = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // テーブル作成（存在しなければ）
        db.CreateTable<TestData>();

        // 1件データを挿入
        db.Insert(new TestData() { value = "Hello SQLite" });

        Debug.Log("SQLite OK: Inserted 1 record.");
    }
}

// テスト用のレコードクラス
public class TestData
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string value { get; set; }
}