# Toggling If IPv6 Is Preferred

[中文版](README-zh.md)

Because I'm living in China and I encountered low traffic during updating the content for the Microsoft Flight Simulator, 
even when I'm using the Netease UU Game Booster.

After some investigation I found out that it is caused by the Windows is preferring IPv6 
when connecting to the remote host. 

The tutorials about solving this kind of issue are all letting us disabling IPv6, 
but I need keeping the IPv6 enabled, and I don't want to solve this issue in such a brute force way.

So I wrote this small utility so others can easily setting if IPv4 is preferred when connecting to the remote host.

Basically I just updated the value in the registration key
`HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters\DisabledComponents`
in order to set preferring using IPv4 on the native interfaces.

You'll have to run it with the Administrator priveleges since it is reading and wring against the Registry.

This utility is written with .NET 6.0 so you might need install the runtime before launching it. 
You can download the .NET 6.0 runtime [here]https://dotnet.microsoft.com/download)

---

Reference document: 
[Guidance for configuring IPv6 in Windows for advanced users - Microsoft](https://docs.microsoft.com/en-us/troubleshoot/windows-server/networking/configure-ipv6-in-windows)
