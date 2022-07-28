# TobiiEtTools

This is a small collection of useful tools for Tobii eyetrackers. 

Usage:
```
TobiiEtTools.exe <switches>

  -i, --info - shows all available info of the avalable (or selected) tracker
  -s, --serial <serial> - selects a unique tracker with the specified serial number
  -f, --frequency <freq> - sets the frequency of the selected tracker to the specified frequency (if supported)
  -n, --name <name> - sets the device name of the selected tracker (if supported)
```

Sample output 
```
> TobiiEtTools.exe -i -s TPSP1-010107101943

Serialnumber: TPSP1-010107101943
Model: Tobii Pro Spectrum
Address: tet-tcp://192.168.0.112/
Name: TPSP1-010107101943
Firmware version: 2.6.1-orbicularis-0
Runtime version: Legacy TTP (4.19.0/3)
License level: Professional
Frequency: 1200
Supported frequencies: 60, 120, 150, 300, 600, 1200
Capability [CanSetDisplayArea]: True
Capability [HasExternalSignal]: True
Capability [HasEyeImages]: True
Capability [HasGazeData]: True
Capability [HasHMDGazeData]: False
Capability [CanDoScreenBasedCalibration]: True
Capability [CanDoHMDBasedCalibration]: False
Capability [HasHMDLensConfig]: False
Capability [CanDoMonocularCalibration]: True
Capability [HasEyeOpennessData]: True
Eyetracking Mode: great_ape
Supported modes: human, monkey, great_ape
```
