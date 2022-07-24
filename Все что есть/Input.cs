using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Все_что_есть
{
    static class Input
    {
        static DateTime time = DateTime.Now;
        public static void Start(Form form)
        {
            form.KeyPreview = true;
            form.KeyDown += KeyDown;
            form.KeyUp += KeyUp;
            form.MouseDown += MouseDown;
            form.MouseUp += MouseUp;
           
        }
        public static void Clear()
        {
            Inputs.Clear();
            mInputs.Clear();
        }
        public static List<Keys> Inputs = new List<Keys>();
        public static List<MouseButtons> mInputs = new List<MouseButtons>();
        static void KeyUp(object obj, KeyEventArgs e) { Inputs.Remove(e.KeyCode); }
        static void KeyDown(object obj, KeyEventArgs e) { if (!Inputs.Contains(e.KeyCode)) Inputs.Add(e.KeyCode); time = DateTime.Now; }
        static void MouseDown(object obj, MouseEventArgs e) { if (!mInputs.Contains(e.Button)) { mInputs.Add(e.Button); time = DateTime.Now; } }
        static void MouseUp(object obj, MouseEventArgs e) { mInputs.Remove(e.Button); }
        public static bool GetKeyDown(Keys keycode)
        {
            bool flag1 = Inputs.Contains(keycode);
            if(time.Millisecond != DateTime.Now.Millisecond)
            Inputs.Remove(keycode);
            return flag1;
        }
        public static bool GetKeyDown(MouseButtons keycode)
        {
            bool flag1 = mInputs.Contains(keycode);
            if (time.Millisecond != DateTime.Now.Millisecond)
                mInputs.Remove(keycode);
            return flag1;
        }
        public static bool GetKeyUp(MouseButtons keycode)
        {
            return mInputs.Contains(keycode);
        }
        public static bool GetKeyUp(Keys keycode)
        {
            return Inputs.Contains(keycode);
        }
    }
}
