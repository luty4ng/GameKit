using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSet
{
    private List<Command> commands;
    private static CommandSet emptyCommand;
    public static CommandSet emptyCommandSet
    {
        get{
            if(emptyCommand ==null)
                emptyCommand = new CommandSet();
            return emptyCommand;
        }
    }
    public bool hasCommand
    {
        get
        {
            if (commands == null)
                return false;
            return commands.Count > 0;
        }
    }
    public CommandSet()
    {
        commands = new List<Command>();
    }
    public void AddCommand(Command command)
    {
        if (commands == null)
            commands = new List<Command>();
        commands.Add(command);
    }
    public void ExcuteCommand(TimeState timeState)
    {
        if (commands.Count > 1)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                if(timeState == TimeState.Forward)
                    commands[i].Do();
                else if(timeState == TimeState.Forward)
                    commands[i].DoReverse();
            }
        }
    }

    public void Clear()
    {
        commands.Clear();
    }

    
}