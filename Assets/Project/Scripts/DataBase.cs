using System;
using System.IO;
using UnityEngine;
using SQLite4Unity3d;

public class DataBase
{
    private SQLiteConnection connection;

    public DataBase()
    {
        string path = Path.Combine(Application.persistentDataPath, "game_data.db");

        // DBが無ければ作成
        connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // テーブル作成（なければ作る）
        connection.CreateTable<ClearTimeRecord>();
    }

    // クリアタイムの保存
    public void InsertClearTime(float clearTime)
    {
        var record = new ClearTimeRecord()
        {
            time = clearTime,
            created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        connection.Insert(record);

        Debug.Log($"Saved Clear Time: {clearTime}");
    }

    // 履歴を全部取得
    public TableQuery<ClearTimeRecord> GetAll()
    {
        return connection.Table<ClearTimeRecord>();
    }

}
