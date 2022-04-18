// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using WindowsHookEx;
using WindowsHookEx.Implementation;

namespace WindowsHook.Rx
{
    public class KeyWithState : Tuple<Keys, KeyboardState>
    {
        public KeyWithState(Keys key, KeyboardState state)
            : base(key, state)
        {
        }

        public Keys KeyCode
        {
            get { return Item1; }
        }

        public KeyboardState State
        {
            get { return Item2; }
        }

        public bool Matches(Combination combination)
        {
            return
                KeyCode == combination.TriggerKey &&
                State.AreAllDown(combination.Chord);
        }
    }
}