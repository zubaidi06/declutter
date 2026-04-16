using Declutter;

namespace Declutter.Services;

public class OrganizerService
{
    private RuleService ruleset;

    public OrganizerService(RuleService ruleset)
    {
        this.ruleset = ruleset;
    }
    public void Organize(String path)
    {
        if(!Directory.Exists(path))
        {
            Console.WriteLine("Directory does not exist.");
            return;
        }
        List<Rule> rules = ruleset.GetRules();
        string[] files = Directory.GetFiles(path);
        foreach(string file in files)
        {
            string type = Path.GetExtension(file);
            foreach(Rule rule in rules)
            {
                if(type == rule.Extension)
                {
                    MoveFile(file, path, rule.Category);
                }
            }
        }
    }
    private void MoveFile(string file, string path, string cat)
    {
        string newFolder = Path.Combine(path, cat);
        if(!Directory.Exists(newFolder))
        {
            Directory.CreateDirectory(newFolder);
        }
        //because we need to extract the name from "file", which contains a path
        File.Move(file, Path.Combine(newFolder, Path.GetFileName(file)));
    }
}