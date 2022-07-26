﻿// See https://aka.ms/new-console-template for more information

using CommandLine;
using Tobii.Research;

Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o => { new Main(o).Run(); });

public class Main
{
    private readonly Options _options;

    public Main(Options options)
    {
        _options = options;
    }

    public void Run()
    {
        var trackers = Tobii.Research.EyeTrackingOperations.FindAllEyeTrackers().ToList();
        Thread.Sleep(TimeSpan.FromSeconds(2));
        trackers = Tobii.Research.EyeTrackingOperations.FindAllEyeTrackers().ToList();
        if (!string.IsNullOrEmpty(_options.Serial))
            trackers = trackers.Where(t => t.SerialNumber.Contains(_options.Serial)).ToList();

        if (_options.Info)
        {
            foreach (var t in trackers)
                PrintInfo(t);
        }
        if (_options.Frequency != 0 && EnsureSingleTracker(trackers))
        {
            ChangeFrequency(trackers.First());
        }

        if (!string.IsNullOrEmpty(_options.Name) && EnsureSingleTracker(trackers))
        {
            ChangeName(trackers.First());
        }

        if (!string.IsNullOrEmpty(_options.Mode) && EnsureSingleTracker(trackers))
        {
            ChangeMode(trackers.First());
        }
    }

    private void ChangeName(IEyeTracker tracker)
    {
        var newName = string.IsNullOrEmpty(_options.Name) ? tracker.SerialNumber : _options.Name;
        var oldName = tracker.DeviceName;
        try
        {
            tracker.SetDeviceName(newName);
        }
        catch
        {
        }

        if (tracker.DeviceName == newName)
            Console.WriteLine($"Changed device name from [{oldName}] to [{newName}]");
        else
        {
            Console.WriteLine("Setting device name failed");
        }
    }

    private void ChangeMode(IEyeTracker tracker)
    {
        var oldMode = tracker.GetEyeTrackingMode();
        try
        {
            tracker.SetEyeTrackingMode(_options.Mode);
        }
        catch
        {
        }

        if (tracker.GetEyeTrackingMode() == _options.Mode)
            Console.WriteLine($"Changed eyetracking mode from [{oldMode}] to [{_options.Mode}]");
        else
        {
            Console.WriteLine("Setting eye tracking mode failed");
        }
    }

    private void ChangeFrequency(IEyeTracker tracker)
    {
        try
        {
            tracker.SetGazeOutputFrequency(_options.Frequency);
        }
        catch
        {
        }

        if ((int)tracker.GetGazeOutputFrequency() == _options.Frequency)
            Console.WriteLine("Changed frequency " + _options.Frequency);
        else
        {
            Console.WriteLine("Setting frequency failed");
        }
    }

    private void PrintInfo(IEyeTracker eyeTracker)
    {
        Console.WriteLine($"Serialnumber: {eyeTracker.SerialNumber}");
        Console.WriteLine($"Model: {eyeTracker.Model}");
        Console.WriteLine($"Address: {eyeTracker.Address}");
        Console.WriteLine($"Name: {eyeTracker.DeviceName}");
        Console.WriteLine($"Firmware version: {eyeTracker.FirmwareVersion}");
        Console.WriteLine($"Runtime version: {eyeTracker.RuntimeVersion}");
        var da = eyeTracker.GetDisplayArea();
        Console.WriteLine($"DisplayArea height: {da.Height:F1} mm");
        Console.WriteLine($"DisplayArea width: {da.Width:F1} mm");
        var diag = Math.Sqrt(da.Height * da.Height + da.Width * da.Width) / 10 / 2.54;
        Console.WriteLine($"DisplayArea size: {diag:F1}\"");
        var m = ((IEyeTrackerMaintenance)eyeTracker);
        Console.WriteLine($"License level: {m.GetLicenseLevel()}");
        try
        {
            if (m.TryReadLicenseFromDevice(out var key))
                Console.WriteLine($"Device has a stored license");
        }
        catch
        {
        }

        Console.WriteLine($"Frequency: {eyeTracker.GetGazeOutputFrequency()}");
        if (eyeTracker.GetAllGazeOutputFrequencies().Count > 1)
            Console.WriteLine($"Supported frequencies: " + string.Join(", ", eyeTracker.GetAllGazeOutputFrequencies().OrderBy(f => f)));
        foreach (var c in Enum.GetValues<Capabilities>().Where(cap => cap != Capabilities.None))
        {
            Console.WriteLine($"Capability [{c}]: " + ((eyeTracker.DeviceCapabilities & c) == c));
        }

        if (eyeTracker.GetAllEyeTrackingModes().Count > 1)
        {
            Console.WriteLine($"Eyetracking Mode: {eyeTracker.GetEyeTrackingMode()}");
            Console.WriteLine($"Supported modes: " + string.Join(", ", eyeTracker.GetAllEyeTrackingModes()));
        }
        Console.WriteLine();
    }

    private bool EnsureSingleTracker(List<IEyeTracker> trackers)
    {
        if (trackers.Count == 0)
            Console.WriteLine("No eye trackers found");
        if (trackers.Count > 1)
            Console.WriteLine("More than one eye tracker detected, use the -s switch to specify which tracker to modify");
        return trackers.Count == 1;
    }
}