# .NET 8 AoT Minimal API with Raspberry Pi 3B+
This is a sample project to demonstrate how to use .NET 8 AoT Minimal API with Raspberry Pi 3B+.

## Prerequisites
- Raspberry Pi 3B+ with Raspbian OS (Legacy, 64bit) Lite
- .NET 8 SDK
- Docker and Visual Studio Code with Dev Container extension

## How to run
1. Clone this repository
2. Open this repository with Visual Studio Code
3. Open workspace with Dev Container
4. Run `dotnet publish -c Release`
5. copy `./bin/Release/net8.0/linux-arm64/publish/` to Raspberry Pi
6. Run `./rpi` on Raspberry Pi

## `run-api.service`
This is a sample service file to run `rpi` on Raspberry Pi as a service on boot.
1. Edit `run-api.service` to replace `ExecStart` and `WorkingDirectory` with the path to `rpi`
2. Copy `run-api.service` to `/etc/systemd/system/` and run `sudo systemctl daemon-reload` to reload the service.
3. Run `sudo systemctl enable run-api.service` to enable this service.
4. Run `sudo systemctl start run-api.service` to start this service without rebooting.
5. Run `sudo systemctl status run-api.service` to check the status of this service.
6. Run `journalctl -u run-api.service` to check the logs of this service.