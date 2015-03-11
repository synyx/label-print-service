# Label Print Service for DYMO Printers

This project provides a Windows service that lets you print tape labels on a DYMO LabelWriter 450 DUO Tape via a HTTP request.

## Getting Started

Clone or fork this repository.

## Building the Project

You will need to have the DYMO Label Software SDK installed as the SDK is neccessary to communicate with DYMO Label printers. Having the SDK installed you can build the project with Visual Studio.

## Installing the Service

Open the Developer Command Prompt from the Visual Studio Tools as administrator and navigate to the PrinterServer executable. You can than install the service with the following command:
```
installutil.exe PrintServer.exe
```

More information about installing services can be found at [msdn](https://msdn.microsoft.com/en-us/library/sd8zc8ha%28v=vs.110%29.aspx).

## Using the Service

Upon service start the service listens for HTTP connections from anywhere on port 80. If you want to use these settings, you may have to adjust firewall settings. If you want to use another bind address and/or port you need to pass them as start parameters to the service.

A print job can be invoked via a GET request to ```http://<yourhost>/print?label=foo```. This will print 'foo' with your DYMO LabelWriter 450 DUO Tape. The default tape size is 24mm and can be adjusted via the url parameter tape:
```http://<yourhost>/print?label=foo&tape=Tape12mm```. If no label is specified in the request, the serivce will respond with 400 (Bad Request), else with 200.