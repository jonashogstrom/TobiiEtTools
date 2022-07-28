using CommandLine;

public class Options
{
    [Option('n', "name", Required = false, HelpText = "Sets the name of an eyetracker (if supported).")]
    public string Name { get; set; }
    [Option('s', "serial", Required = false, HelpText = "The serial number of hte tracker to modify.")]
    public string Serial { get; set; }
    [Option('f', "frequency", Required = false, HelpText = "Sets the frequency of the tracker.")]
    public int Frequency { get; set; }
    [Option('m', "mode", Required = false, HelpText = "Sets the eyetracking mode of the tracker.")]
    public string Mode { get; set; }
    [Option('i', "info", Required = false, HelpText = "Print tracker info.")]
    public bool Info { get; set; }
}