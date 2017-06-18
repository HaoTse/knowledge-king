using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class CurrentPlayer
    {
        public static bool isLogin = true;
        private static string name = "king";

        public static void Login(string player)
        {
            isLogin = true;
            name = player;
        }

        public static void Logout()
        {
            isLogin = false;
            name = "";
        }

        public static string getPlayer()
        {
            return name;
        }
    }
}
