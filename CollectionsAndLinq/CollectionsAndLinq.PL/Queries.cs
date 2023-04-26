using System.Reflection;
using System.Runtime.CompilerServices;
using CollectionsAndLinq.BL.Services;

namespace CollectionsAndLinq.PL;

public class Queries
{
    private readonly Client client;
    private IList<MethodInfo> methods;

    public Queries()
    {
        this.methods = new List<MethodInfo>();
        client = new Client();
        AddMethods();
        
    }

    private void AddMethods()
    {
        var methods = this.GetType().GetMethods();
        foreach (var method in methods)
        {
            if (method.ReturnType.Equals(typeof(Task<string>)))
            {
                this.methods.Add(method);
            }
        }
    }
    private MethodInfo PickMethod()
    {
        var random = new Random();
        var index = random.Next(methods.Count());
        return methods[index];
    }

    public async Task<int> MarkRandomTaskWithDelay(int delay, bool writeResToConsole = false)
    {
        var method = PickMethod();
        var callmethod = method.Invoke(this, new object[] { }) as Task<string>;
        if (callmethod == null)
        {
            throw new Exception("No suitable method");
        }

        await Task.Delay(delay);
        var res = await callmethod;
        if (writeResToConsole)
        {
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            Console.WriteLine(res);
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
        }
        return callmethod.Id;
    }
    public Task<string> GetProjects()
    {
        return client.Get("Projects");
    }
    public Task<string> GetUsers()
    {
        return client.Get("Users");
    }
    public Task<string> GetTeams()
    {
        return client.Get("Teams");
    }
    public Task<string> GetTasks()
    {
        return client.Get("Tasks");
    }
}
