/*
manager.cs is the entry point.
1. it reads cli arguments
2. calls the appropriate service.
*/
using Declutter.Services;


class Manager
{
    private static RuleService ruleset = new RuleService();
    // private static OrganizerService warehouse = new OrganizerService(ruleset);

    static void Main(string[] args)
    {
        //very basic error handling
        if (args.Length > 4)
        {
            Console.WriteLine("Incorrect command");
        }

        //welcome page
        if (args.Length == 0)
        {
            PrintWelcome();
            return;
        }

        //all other comands
        switch(args[0].ToLower())
        {
            case "help":
                PrintHelp();
                break;
            // case "organize":
            //     OrganizeManager(args);
            //     break;
            case "rules":
                RulesManager(args, ruleset);
                break;
            default:
                Console.WriteLine("Hmmm...");
                Console.WriteLine("");
                PrintHelp();
                break;
        }
        
    }

    // static void OrganizeManager(string[] args)
    // {
    //     if(args.Length < 2)
    //     {
    //         Console.WriteLine("You must provide a folder to organize");
    //         Console.WriteLine("");
    //         PrintHelp();
    //     }
    //     warehouse.Organize(args[1]);
    // }

    static void RulesManager(string[] args, RuleService ruleset)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Specify a rules command.");
            Console.WriteLine("");
            PrintHelp();
            return;
        }

        switch(args[1].ToLower())
        {
            case "list":
                if(args.Length > 2)
                {
                    Console.WriteLine("Too many commands.");
                    Console.WriteLine("");
                    PrintHelp();
                }
                ruleset.ListRules();
                break;
            case "reset":
                if(args.Length > 2)
                {
                    Console.WriteLine("Too many commands.");
                    Console.WriteLine("");
                    PrintHelp();
                }
                ruleset.ResetRules();
                break;
            case "add":
                if(args.Length != 4)
                {
                    Console.WriteLine("Improper use of add command.");
                    Console.WriteLine("");
                    PrintHelp();
                }
                ruleset.AddRule(args);
                break;
            case "update":
                if(args.Length != 4)
                {
                    Console.WriteLine("Improper use of update command.");
                    Console.WriteLine("");
                    PrintHelp();
                }
                ruleset.UpdateRule(args);
                break;
            case "delete":
                if(args.Length != 3)
                {
                    Console.WriteLine("Improper use of delete command.");
                    Console.WriteLine("");
                    PrintHelp();
                }
                ruleset.DeleteRule(args);
                break;
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("Declutter");
        Console.WriteLine("");
        Console.WriteLine("List of commands:");
        Console.WriteLine("");
        Console.WriteLine("ORGANIZE");
        Console.WriteLine("");
        Console.WriteLine("declutter organize <path>");
        Console.WriteLine("     organizes the path based on your ruleset");
        Console.WriteLine("");
        Console.WriteLine("CREATE, READ, UPDATE, DELETE THE RULESET");
        Console.WriteLine("");
        Console.WriteLine("declutter rules list");
        Console.WriteLine("     lists every rule in the json");
        Console.WriteLine("declutter rules add <extension> <category>");
        Console.WriteLine("     creates a new rule in your json, doesn't work for existing extensions");
        Console.WriteLine("declutter rules update <extension> <category>");
        Console.WriteLine("     updates a rule, use this for an extension which already exists");
        Console.WriteLine("declutter rules remove <extension>");
        Console.WriteLine("     delete an extension's rule");
        Console.WriteLine("declutter rules reset");
        Console.WriteLine("     reset to default ruleset");

    }

    static void PrintWelcome(){
        Console.WriteLine("Welcome to Declutter!");
    }
}