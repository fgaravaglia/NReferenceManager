using NReferenceManager.Cli;

Console.WriteLine("********************************************************");
Console.WriteLine("           NReferenceManager.Cli - Version 0.9");
Console.WriteLine("********************************************************");
var manager = new ReferenceManager();
manager.ParseArguments(args);
Environment.Exit(manager.Run());