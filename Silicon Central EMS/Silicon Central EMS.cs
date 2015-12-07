using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation;

namespace Silicon_Central_EMS
{
    class Program
    {
        /// <summary>
        /// Main access point to the program, instantiates object of type UIMenu and launches the user-friendly constructed menu
        /// </summary>
        static void Main(string[] args)
        {
            UIMenu menu = new UIMenu();
            menu.Launch();
        }
    }
}
