using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OOOpj
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entity.OOOpjEntities DB;
        public static string Name { get; set; }
        public static string Password { get; set; }
        public static string RName { get; set; }
        public static string RPassword { get; set;}
    }
}
