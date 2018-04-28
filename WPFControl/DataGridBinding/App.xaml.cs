using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace DataGridBinding
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private PeoplesViewModel mPeoplesViewModel = new PeoplesViewModel ( );

        public PeoplesViewModel PeoplesViewModel
        {
            get { return mPeoplesViewModel; }
        }

        protected override void OnStartup ( StartupEventArgs e )
        {
            base.OnStartup ( e );
            Person yafeyaPerson = new Person
            {
                Age = 27,
                Sex = Sex.Male
            };
            Person tealcPerson = new Person
            {
                Age = 103,
                Sex = Sex.Male
            };
            People yafeya = new People
            {
                Name = "yafeya",
                Person = yafeyaPerson
            };
            People tealc = new People
            {
                Name = "Teal'c",
                Person = tealcPerson
            };
            mPeoplesViewModel.Peoples.Add ( yafeya );
            mPeoplesViewModel.Peoples.Add ( tealc );
        }
    }
}
