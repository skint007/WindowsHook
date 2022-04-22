# WindowsHook
Based on [WindowsHook](https://github.com/topstarai/WindowsHook) by topstarai. Added injected flag detection to both mouse events and keyboard events. This is useful if for example you want to preform continuous mouse clicks of the same type of the currently held down mouse button. Supports .Net 4.8,.Net 6+, WinForm, WPF etc.

[![nuget][nuget-badge]][nuget-url]

 [nuget-badge]: https://img.shields.io/badge/nuget-v1.1.1-blue.svg
 [nuget-url]: https://www.nuget.org/packages/WindowsHookEx

![Mouse and Keyboard Hooking Library in c#](/mouse-keyboard-hook-logo64x64.png)

## What it does?

This library allows you to tap keyboard and mouse, to detect and record their activity even when an application is inactive and runs in background.

## Prerequisites

 - **Windows:** .Net 4.8,.Net 6+, Windows Desktop Apps(WinForm, WPF etc.)

## Installation and sources

<pre>
  nuget install WindowsHookEx
</pre>

 - [NuGet package][nuget-url]
 - [Source code][source-url]

 [source-url]: https://github.com/skint007/WindowsHookEx

 ## Injected flag usage
 
 ```csharp
private IKeyboardMouseEvents globalHook = Hook.GlobalEvents();
private bool IsLeftMouseDown;
private bool IsRightMouseDown;

private void Subscribe()
{
	globalHook.MouseDownExt += GlobalHook_MouseDownExt;
	globalHook.MouseUpExt += GlobalHook_MouseUpExt;
}

private void UnSubscribe()
{
	globalHook.MouseDownExt -= GlobalHook_MouseDownExt;
	globalHook.MouseUpExt -= GlobalHook_MouseUpExt;
	
	globalHook.Dispose();
}

private void GlobalHook_MouseDownExt(object? sender, MouseEventExtArgs e)
{
	switch (e.Button)
	{
		case MouseButtons.Left: if (!e.IsInjected) IsLeftMouseDown = true; break;
		case MouseButtons.Right: if (!e.IsInjected) IsRightMouseDown = true; break;
	}
	
	if(IsLeftMouseDown)
	{
		_ = FastClick();
	}
}

private void GlobalHook_MouseUpExt(object? sender, MouseEventExtArgs e)
{
	switch (e.Button)
	{
		case MouseButtons.Left: if (!e.IsInjected) IsLeftMouseDown = false; break;
		case MouseButtons.Right: if (!e.IsInjected) IsRightMouseDown = false; break;
	}
}

private async Task FastClick()
{
	await Task.Run(() =>
	{
		while(IsLeftMouseDown)
		{
			//Send left mouse click code here
			Thread.Sleep(100);
		}
	});
}
 ```
 
 ## Usage

 ```csharp
 private IKeyboardMouseEvents m_GlobalHook;

 public void Subscribe()
 {
     // Note: for the application hook, use the Hook.AppEvents() instead
     m_GlobalHook = Hook.GlobalEvents();

     m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
     m_GlobalHook.KeyPress += GlobalHookKeyPress;
 }

 private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
 {
     Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
 }

 private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
 {
     Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

     // uncommenting the following line will suppress the middle mouse button click
     // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
 }

 public void Unsubscribe()
 {
     m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
     m_GlobalHook.KeyPress -= GlobalHookKeyPress;

     //It is recommened to dispose it
     m_GlobalHook.Dispose();
 }
 ```
(also have a look at the Demo app included with the source)

## How it works?

This library attaches to windows global hooks, tracks keyboard and mouse clicks and movement,please note this don't support common .Net events with KeyEventArgs and MouseEventArgs. this is the difference with WindowsHook project. But you can still easily retrieve any information you need:
 * Mouse coordinates
 * Mouse buttons clicked
 * Mouse drag actions
 * Mouse wheel scrolls
 * Key presses and releases
 * Special key states

 Additionally, there are `MouseEventExtArgs` and `KeyEventExtArgs` which provide further options:
 * Input suppression
 * Timestamp
 * IsMouseDown/Up
 * IsKeyDown/Up.

## Quick contributing guide

 - Fork and clone locally
 - Create a topic specific branch. Add some nice feature.
 - Send a Pull Request to spread the fun!

## License

The MIT license see: [LICENSE.txt](/LICENSE.txt)
