using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Color = System.Drawing.Color;
using Font = SharpDX.Direct3D9.Font;
using System.Timers;
using LeagueSharp;
using LeagueSharp.Common;

using System.Threading;
using SharpDX;


namespace LimitedShat
{

    internal class Program
    {
        #region DeclarationVar
        static Menu _main;
        static Vector2 _screenPos;

        static List<string> _allowed = new List<string> { "/msg", "/r", "/w", "/surrender", "/nosurrender", "/help", "/dance", "/d", "/taunt", "/t", "/joke", "/j", "/laugh", "/l", "/ff" };
        private static int _Count = 3;

        #endregion


        public static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

       static void Game_OnGameLoad(EventArgs args)
        {

            (_main = new Menu("LimitedShat", "LimitedShat", true)).AddToMainMenu();
           
            
            _main.AddItem(new MenuItem("drawing", "Drawing").SetValue(true));
            var posX = new MenuItem("positionx", "Position X").SetValue(new Slider(Drawing.Width - 100, 0, Drawing.Width - 20));
            var posY = new MenuItem("positiony", "Position Y").SetValue(new Slider(Drawing.Height / 2, 0, Drawing.Height - 20));

            posX.ValueChanged += (sender, arg) => _screenPos.X = arg.GetNewValue<Slider>().Value;
            posY.ValueChanged += (sender, arg) => _screenPos.Y = arg.GetNewValue<Slider>().Value;
            _main.AddItem(posX);
            _main.AddItem(posY);


            _screenPos.X = posX.GetValue<Slider>().Value;
            _screenPos.Y = posY.GetValue<Slider>().Value;


            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnInput += Game_OnInput;
 
            timerTick();

            Notifications.AddNotification("LimitedShat Loaded!", 1000);
        }

    

        static void Drawing_OnDraw(EventArgs args)
        {
            if (!_main.Item("drawing").GetValue<bool>()) return;
            Drawing.DrawText(_screenPos.X, _screenPos.Y, Color.Yellow, "Messages Available: " + (_Count.ToString()) + "/5");
            
        }

     
        static void Game_OnInput(GameInputEventArgs args)
        {
            if (_Count < 0)
                _Count = 0;



            if ((_Count == 0)&& (!args.Input.Equals("")))
            {
                args.Process = false;
                Notifications.AddNotification(new Notification("Limite exceeded!", 3000).SetTextColor(Color.White).SetBoxColor(Color.Black));
            }

            if (_allowed.Any(str => args.Input.StartsWith(str)))
            {
                args.Process = true;
            }
            if (!_allowed.Any(str => args.Input.StartsWith(str))&& (_Count > 0) && (!args.Input.Equals("")))
            {
                _Count--;
                args.Process = true;
            }
           



        }

        static void timerTick()
        {
            var enableTimer = new System.Timers.Timer(240000); // 4 minutes = 240 000 ms
            enableTimer.Elapsed += enableTimer_Elapsed;
            enableTimer.Enabled = true;
            enableTimer.Start();
        }
        static void enableTimer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            increment();
        }
             static void increment()
        {

            if(_Count < 5) 
            {
                _Count++;
               
            }
          
        }
         
    }
}
