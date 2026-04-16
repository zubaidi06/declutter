using Declutter;
using System.Text.Json;

namespace Declutter.Services;

public class RuleService
{
    private const string RULESET = "ruleset.json";

    public List<Rule> GetRules()
    {
        if (!File.Exists(RULESET))
        {
            ResetRules();
        }
        string json = File.ReadAllText(RULESET);
        //? means it can be null if the deserialize
        List<Rule>? rules = JsonSerializer.Deserialize<List<Rule>>(json);
        if(rules != null){
            return rules;
        }
        return new List<Rule>();//just give empty list of rules if it was null
    }
    //call after playing with the json
    public void SaveRules(List<Rule> rules)
    {
        string json = JsonSerializer.Serialize(rules);
        File.WriteAllText(RULESET, json);
    }

    public void ListRules()
    {
        List<Rule> rules = GetRules();
        if(rules.Count == 0)
        {
            Console.WriteLine("No rules in the JSON, or its been deleted.");
            return;
        }
        foreach (Rule rule in rules)
        {
            Console.WriteLine($"{rule.Extension} -> {rule.Category}");
        }
    }
    public void AddRule(string[] args)
    {
        //check the rule doesn't exist
        List<Rule> rules = GetRules();
        foreach(Rule rule in rules)
        {
            if(rule.Extension == args[2])
            {
                Console.WriteLine("Rule exists. Use 'update' instead.");
                return;
            }
        }
        rules.Add(new Rule
        {
            Extension = args[2],
            Category = args[3]
        });
        SaveRules(rules);
    }
    public void UpdateRule(string[] args)
    {
        List<Rule> rules = GetRules();

        foreach (Rule rule in rules)
        {
            if (rule.Extension == args[2])
            {
                rule.Category = args[3];
                SaveRules(rules);
                return;
            }
        }

        Console.WriteLine("No rule found for that extension.");
    }
    public void DeleteRule(string[] args)
    {
        List<Rule> rules = GetRules();

        for (int i = 0; i < rules.Count; i++)
        {
            if (rules[i].Extension == args[2])
            {
                rules.RemoveAt(i);
                SaveRules(rules);
                return;
            }
        }

        Console.WriteLine("No rule found for that extension.");
    }
    public void ResetRules()
    {
        //create a new list, add to it, then pass that to SaveRules
        List<Rule> defaultRules = new List<Rule>
        {
            new Rule { Extension = ".pdf",  Category = "Documents" },
            new Rule { Extension = ".txt",  Category = "Documents" },
            new Rule { Extension = ".jpg",  Category = "Images" },
            new Rule { Extension = ".png",  Category = "Images" },
            new Rule { Extension = ".mp3",  Category = "Audio" },
            new Rule { Extension = ".mp4",  Category = "Video" },
            new Rule { Extension = ".mov",  Category = "Video" },
            new Rule { Extension = ".c",  Category = "Code" }
        };

        SaveRules(defaultRules);
    }
}