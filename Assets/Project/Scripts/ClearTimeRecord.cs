using SQLite4Unity3d;

public class ClearTimeRecord
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public float time { get; set; }

    public string created_at { get; set; }
}
