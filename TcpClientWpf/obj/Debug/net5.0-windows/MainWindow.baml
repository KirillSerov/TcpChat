   M S B A M L   `   `   `  ????G  CTcpClientWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TcpClientWpfTcpClientWpf  5   	       TcpClientWpf.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TcpClientWpf     5   	   -   5          ? Title$  TcpChatClient??5   	   $	??450??6   $	??800??6/   $	??450??6>   $	??800??6K   $??14(?6W   .??5	      ? .+?5
   
   ?  ? RowDefinitions 5      ?? 5      $??* ?6    ?? 5      $??3* ?6    ?? 5      $??* ?6    .+?5      ? $??0??6    ? ColumnDefinitions 5      ?? 5      $??2* ?6'   ?? 5      $??* ?6'   .+?5      ?  5      ?? 5      $
??0.3* ?6+   ?? 5      $
??0.3* ?6+   ?? 5      $??* ?6+   .+?5      ?? -   5         ??Name$ ConnectionStatus??5      $??64??66   $??64??6A   $ ??Resources/Disconnected.png??6M   ?? 5      $??
Username: ??6    $??Right=?62   $??Center=?6N   $??1??6i   ?? 5      -   5         ??Name$ Username??5      $??Center=?60   $??2??6K   $??0q?6[    ?? 	MaxLength$ 20??6f   $??10q?6u   5!      ? 5#      $??2??6    5$      ?? 5%      ?? 5&      .+?5(      ?? -   5         ??Name$ Connect??5(   *   $??0??6   $??Connect??6;   $??10q?6c   ?? 5)      -   5        $ 
Disconnect??5)   *   $??1??6   $??
Disconnect??6>   $??10q?6l   ${?False??6x   5*      5+      ? 5-      $??1??6   .+?5.      ?? -   5        $
 Chat??5.      $??Stretch=?6(   $??10q?6F    ?? TextWrapping$
 Wrap=?6R    ?? 
IsReadOnly$
 True??6f   	 ?? IsReadOnlyCaretVisible$	 false??5/      "
 ?? VerticalScrollBarVisibility$

 Auto=?61   50      ? 52      $??2??6    53      ?? 54      $??3* ?6'   ?? 55      $??* ?6'   .+?58      ?? -   5        $ Message??58      $??Stretch=?6+   $??10q?6I   $

 Auto=?6U   $
 Wrap=?6x   $??0??6?   ?? 59      -   5        $
 Send??59      $??Отправить??6'   $??10q?6;   $??1??6Z   ${?False??6j   5:      5;      5=      5>      