using System;

namespace Coorth.Platforms; 

[Flags]
public enum PlatformTypes {
    Other = 0,
    
    Windows = 0x01,
    Linux = 0x02,
    Android = 0x03,
    IOS = 0x04,
    MacOS = 0x05,
    
    Player = 0b01 << 16,
    Editor = 0b10 << 16,
    Server = 0b11 << 16,

    WindowsPlayer = Windows | Player,
    WindowsEditor = Windows | Editor,
    WindowsServer = Windows | Server,
    
    LinuxPlayer = Linux | Player,
    LinuxEditor = Linux | Editor,
    LinuxServer = Linux | Server,
    
    MacOSPlayer = MacOS | Player,
    MacOSEditor = MacOS | Editor,
    MacOSServer = MacOS | Server,
}