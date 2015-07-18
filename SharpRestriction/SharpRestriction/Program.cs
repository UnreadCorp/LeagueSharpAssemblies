using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Color = System.Drawing.Color;
using Font = SharpDX.Direct3D9.Font;
using System.Timers;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Sandbox;

//Thanks to Kurisu, werdbrian4, and Porncore for their help!
// yes i've no idea on how to write a code decently.
namespace SharpRestriction
{

    internal class Program
    {
        #region DeclarationVar
        static Menu _main;     
        static List<string> _allowed = new List<string> { "/msg", "/r", "/w", "/surrender", "/nosurrender", "/help", "/dance", "/d", "/taunt", "/t", "/joke", "/j", "/laugh", "/l", "/ff"};

        #endregion
        public static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        static void Game_OnGameLoad(EventArgs args)
        {
           
            (_main = new Menu("NO MORE TALKING", "NO MORE TALKING", true)).AddToMainMenu();
            Notifications.AddNotification("SharpRestriction Loaded!", 1000);
            Game.OnInput += Game_OnInput;
          
        }


        static void Game_OnInput(GameInputEventArgs args)
        {

            args.Process = false;

            if (_allowed.Any(str => args.Input.StartsWith(str)))
            {
                args.Process = true;
            }
  
            if (args.Process == false)
                Notifications.AddNotification(new Notification("Private messages only.", 4000).SetTextColor(Color.White).SetBoxColor(Color.Black));
            return;

        }
    }
}
