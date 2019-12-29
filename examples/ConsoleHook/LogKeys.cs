﻿// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Threading;
using System.Windows.Hook;

namespace ConsoleHook
{
    internal class LogKeys
    {
        public static void Do(Action  quit)
        {
            Console.WriteLine("Press Q to quit.");
            Hook.GlobalEvents().KeyPress += (sender, e) =>
            {
                Console.Write(e.KeyChar);
                if (e.KeyChar == 'q') quit();
            };
        }
    }
}