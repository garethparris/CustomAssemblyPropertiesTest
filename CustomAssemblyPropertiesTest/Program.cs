// See https://aka.ms/new-console-template for more information

using System.Reflection;

Console.WriteLine("Hello, World!");

//new("display_name", Bootstrapper.ApplicationName),
//new("display_version", Bootstrapper.ApplicationVersion),
//new("runtime_version", Bootstrapper.RuntimeVersion),
//new("commit", Bootstrapper.GitCommit)
////new("branch", ThisAssembly.Git.Branch),
//// new("timestamp", Bootstrapper.TopAssembly.GetCustomAttributesData())

Console.WriteLine(Bootstrapper.ApplicationName);
Console.WriteLine(Bootstrapper.ApplicationVersion);
Console.WriteLine(Bootstrapper.RuntimeVersion);

Console.WriteLine(Bootstrapper.InformationalVersion);

Console.WriteLine(Bootstrapper.BuildInformation.BuildDate);
Console.WriteLine(Bootstrapper.BuildInformation.GitBranch);
Console.WriteLine(Bootstrapper.BuildInformation.GitCommit);

public static class Bootstrapper
{
    public static Assembly TopAssembly =>
        Assembly.GetEntryAssembly() ?? typeof(Program).Assembly;

    public static string ApplicationName =>
        TopAssembly.GetName().Name ??
        throw new InvalidOperationException("Assembly name could not be resolved");

    public static Version? ApplicationVersion =>
        TopAssembly.GetName().Version;

    public static readonly Version RuntimeVersion = Environment.Version;

    public static readonly string? InformationalVersion = (TopAssembly.GetCustomAttributes(
            typeof(AssemblyInformationalVersionAttribute), true)
        .FirstOrDefault() as AssemblyInformationalVersionAttribute)?.InformationalVersion;

    public static BuildInfo BuildInformation
    {
        get
        {
            var value = InformationalVersion?.Split('+') ?? [];
            return value.Length == 3
                ? new BuildInfo(value[0], value[1], value[2])
                : new BuildInfo(string.Empty, string.Empty, string.Empty);
        }
    }

    public record BuildInfo(string BuildDate, string GitCommit, string GitBranch);
}