using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsAndLinq.BL.Services;

namespace CollectionsAndLinq.PL;

public class ProgramController
{
    private bool isOpen = true;
    private readonly Queries queries;
    public ProgramController()
    {
        queries = new Queries();
    }

    public void Start()
    {
        Hello();
        while (isOpen)
        {
            Perform();
        }
    }
    public async void Perform()
    {
        string command = GetCommand();
        try
        {
            switch (command)
            {
                case "/random":
                    Console.WriteLine($"Id of made task: {await queries.MarkRandomTaskWithDelay(1000, false)}");
                    break;
                case "/projects":
                    Console.WriteLine(await queries.GetProjects());
                    break;
                case "/tasks":
                    Console.WriteLine(await queries.GetTasks());
                    break;
                case "/users":
                    Console.WriteLine(await queries.GetUsers());
                    break;
                case "/teams":
                    Console.WriteLine(await queries.GetTeams());
                    break;
                case "/help":
                    Info();
                    break;
                case "/exit":
                    isOpen = false;
                    break;
                default:
                    Console.WriteLine("No such command");
                    break;
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(ex.Message.ToString());
            Console.WriteLine("----------------------------------------");
        }
    }
    private static void Info()
    {
        Console.WriteLine("Choose command from the list:\n\n" +
            "\t/random - RandomQuery" +
            "\t/projects - GetProjects" +
            "\t/tasks - GetTasks" +
            "\t/users - GetUsers" +
            "\t/teams - GetTeams"
            //"\t/task1 - GetTasksCountInProjectsByUserIdAsync\n" +
            //"\t/task2 - GetCapitalTasksByUserIdAsync\n" +
            //"\t/task3 - GetProjectsByTeamSizeAsync\n" +
            //"\t/task4 - GetSortedTeamByMembersWithYearAsync\n" +
            //"\t/task5 - GetSortedUsersWithSortedTasksAsync\n" +
            //"\t/task6 - GetUserInfoAsync\n" +
            //"\t/task7 - GetProjectsInfoAsync\n" +
            //"\t/task8 - GetSortedFilteredPageOfProjectsAsync\n"
            );
    }
    public void Hello()
    {
        Console.WriteLine("Welcome to our programm. Enter commands...");
    }
    private static string GetCommand()
    {
        Console.Write(">>");
        var command = Console.ReadLine();
        return command!;
    }
}
