﻿# 设定是否首选使用IPv6

[English Version](README.md)

因为我在通过网易UU加速器更新微软模拟飞行的时候遇到了下载速度极慢的问题，一顿网上冲浪之后发现，是因为系统默认通过IPv6连接导致的。

网上的教程都是让用户直接禁用掉IPv6，但因为我需要保持IPv6开启，也不想这么简单粗暴的解决问题，
所以我开发了这个小工具，方便各位遇到同样问题的人可以快速切换成首选使用IPv4连接。

原理是通过修改注册表键值
`HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters\DisabledComponents`
来让系统在本机接口上首选使用IPv4来连接。

---

参考文档：[为高级用户配置 Windows IPv6 的指南 - Microsoft](https://docs.microsoft.com/zh-cn/troubleshoot/windows-server/networking/configure-ipv6-in-windows)