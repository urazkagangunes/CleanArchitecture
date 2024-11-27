namespace App.Repositories;

public class ConnectionStringOption
{
    public const string Key = "ConnectionStrings";
    public string SqlConnection { get; set; } = default!;
}
