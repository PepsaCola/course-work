namespace FortniteHelper.Models
{
    public class GetStatsModel
    {
        public DataStats data { get; set; }
        public string image { get; set; }

    }
    public class DataStats
    {
        public Account account { get; set; }
        public BattlePass battlePass { get; set; }
        public Stats stats { get; set; }
    }
    public class Account
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class BattlePass
    {
        public int level { get; set; }
    }
    public class Stats
    {
        public All all { get; set; }
    }
    public class All
    {
        public Overall overall { get; set; }
        public Solo solo { get; set; }
        public Duo duo { get; set; }
        public Trio trio { get; set; }
        public Squad squad { get; set; }
    }
    public class Overall
    {
        public int wins { get; set; }
        public int top10 { get; set; }
        public int top25 { get; set; }
        public int kills { get; set; }
        public double killsPerMatch { get; set; }
        public double kd { get; set; }
        public int matches { get; set; }
        public double winRate { get; set; }
    }
    public class Solo
    {
        public int wins { get; set; }
        public int top10 { get; set; }
        public int top25 { get; set; }
        public int kills { get; set; }
        public double killsPerMatch { get; set; }
        public double kd { get; set; }
        public int matches { get; set; }
        public double winRate { get; set; }
    }
    public class Duo
    {
        public int wins { get; set; }
        public int top10 { get; set; }
        public int top25 { get; set; }
        public int kills { get; set; }
        public double killsPerMatch { get; set; }
        public double kd { get; set; }
        public int matches { get; set; }
        public double winRate { get; set; }
    }
    public class Trio
    {
        public int wins { get; set; }
        public int top10 { get; set; }
        public int top25 { get; set; }
        public int kills { get; set; }
        public double killsPerMatch { get; set; }
        public double kd { get; set; }
        public int matches { get; set; }
        public double winRate { get; set; }
    }
    public class Squad
    {
        public int wins { get; set; }
        public int top10 { get; set; }
        public int top25 { get; set; }
        public int kills { get; set; }
        public double killsPerMatch { get; set; }
        public double kd { get; set; }
        public int matches { get; set; }
        public double winRate { get; set; }
    }
}
