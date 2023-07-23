// See https://aka.ms/new-console-template for more information
using LearnFreeSQL.Model;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        IFreeSql fsql = DB.Sqlite;

        var t1 = fsql.DbFirst.GetDatabases();
        fsql.Insert<Blog>(new Blog { BlogId = 1, Rating = 212, Url = "baidu" }).ExecuteInserted();
        Console.WriteLine("111");
        
        
    }
}

public class DB
{
    static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
          .UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句,Trace在输出选项卡中查看
          .UseConnectionString(FreeSql.DataType.SqlServer, @"Data Source=Mou-Computer;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=1234560.@")
    .UseAutoSyncStructure(true) //自动同步实体结构【开发环境必备】，FreeSql不会扫描程序集，只有CRUD时才会生成表。      
    .Build());
    public static IFreeSql Sqlite => sqliteLazy.Value;
}