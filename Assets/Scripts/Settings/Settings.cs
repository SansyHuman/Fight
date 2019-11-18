using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public struct KeyBindings
    {
        public readonly KeyCode moveLeft;
        public readonly KeyCode moveRight;
        public readonly KeyCode jump;
        public readonly KeyCode interact;
        public readonly KeyCode attack;

        public KeyBindings(KeyCode moveLeft, KeyCode moveRight, KeyCode jump, KeyCode interact, KeyCode attack)
        {
            this.moveLeft = moveLeft;
            this.moveRight = moveRight;
            this.jump = jump;
            this.interact = interact;
            this.attack = attack;
        }
    }

    public static class PlayerKeyBindings
    {
        public static readonly KeyBindings player1 = new KeyBindings(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Space);
        public static readonly KeyBindings player2 = new KeyBindings(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.KeypadEnter);
    }
}
