using System;
using System.Linq;
using System.Collections.Generic;

namespace OctoScreenMenu
{
    public class GCodeMacroSectionFile : SectionFile
    {
        readonly List<string[]> GCodeCommands = new List<string[]>();

        internal override bool OnAddingLine(string parameter, string value)
        {
            return false;
        }

    }

    public class MenuSectionFile : SectionFile
    {
        public string CommandType { get; private set; }
        public string Title { get; private set; }
        public string Action { get; private set; }

        public readonly List<string> GCodes = new List<string>();
        public readonly List<string> Items = new List<string>();

        string actualParameter;

        public readonly List<string> MenuIds = new List<string>();

        internal override void OnProcessingName(string line)
        {
            base.OnProcessingName(line);
            line = line.Substring(1, line.Length - 2);

            if (line.StartsWith ("menu ")) {
                var ids = line.Substring("menu ".Length).Trim ();
                MenuIds.AddRange(ids.Split (' '));
            }

        }

        internal override bool OnAddingLine (string parameter, string value)
        {
            switch (parameter)
            {
                case "type":
                    actualParameter = parameter;
                    CommandType = value;
                    return true;
                case "name":
                    actualParameter = parameter;
                    Title = value;
                    return true;
                case "action":
                    actualParameter = parameter;
                    Action = value;
                    return true;
                case "gcode":
                    actualParameter = parameter;
                    return false;
                case "items":
                    actualParameter = parameter;
                    return false;
                default:
                    if (actualParameter == "gcode")
                    {
                        GCodes.Add(parameter);
                    }
                    else if (actualParameter == "items")
                    {
                        Items.Add(parameter);
                    }
                    break;
            }
            return false;
        }
    }

    public class StepperSectionFile : SectionFile
    {
        public override void ProcessLine (string line)
        {
            base.ProcessLine (line);
        }
    }

    public class SectionFile
    {
        public static string GetName (string line)
        {
            return line.Substring(1, line.Length - 2);
        }

        protected Dictionary<string, string> lines = new Dictionary<string, string>();

        public string Name { get; private set; }

        internal virtual bool OnAddingLine (string parameter, string value)
        {
            return true;
        }

        internal virtual void OnProcessingName (string line)
        {
            Name = GetName(line);
        }


        public virtual void ProcessLine(string line)
        {
            if (line.StartsWith ("[")) {
                OnProcessingName(line);
            }
            else
            {
                string parameter = null;
                string commandValue = null;
                var indexOf = line.IndexOf(':');
                if (indexOf > -1)
                {
                    parameter = line.Substring(0, indexOf).Trim ();
                    commandValue = line.Substring(indexOf + 1).Trim ();
                }
                else
                {
                    parameter = line.Trim ();
                }
                try
                {
                    if (OnAddingLine (parameter, commandValue)) {
                        lines.Add(parameter, commandValue);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
             
            }
        }
    }

    public class KCfgFile
    {
        readonly public List<SectionFile> Sections = new List<SectionFile>();
        readonly public List<KCfgFile> Included = new List<KCfgFile>();

        public IEnumerable<SectionFile> AllSections ()
        {
            foreach (var item in Sections)
            {
                yield return item;
            }

            foreach (var item in Included)
            {
                foreach (var sec in item.AllSections ())
                {
                    yield return sec;
                }
            }
        }

        public void Load(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            var directoryPath = System.IO.Path.GetDirectoryName(filePath);

            SectionFile current = null;
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace (trimmed))
                    continue;
                if (trimmed.StartsWith("#"))
                    continue;

                if (trimmed.StartsWith("[")) {
                    var name = SectionFile.GetName(trimmed);

                    //section
                    if (name.StartsWith("include "))
                    {
                        var file = name.Substring("include ".Length)
                            .Trim ();
                        var includeFilePath = System.IO.Path.Combine(directoryPath, file);
                        if (System.IO.File.Exists (includeFilePath))
                        {
                            var includeFile = new KCfgFile();
                            includeFile.Load(includeFilePath);
                            Included.Add(includeFile);
                        }

                        continue;
                    }
                    else if (name.StartsWith("stepper"))
                    {
                        current = new StepperSectionFile();
                    }
                    else if (name.StartsWith("gcode_macro"))
                    {
                        current = new GCodeMacroSectionFile ();
                    }
                    else if (name.StartsWith("menu"))
                    {
                        current = new MenuSectionFile();
                    }
                    else
                    {
                        //not implemented
                        current = new SectionFile();
                    }

                    Sections.Add(current);
                }

                if (current == null)
                {
                   Console.WriteLine ("not section in progress");
                   continue;
                }
                current.ProcessLine(trimmed);
            }

            Console.WriteLine("");
        }
    }
}
