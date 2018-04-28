using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace DataGridBinding
{
    public class PeoplesViewModel
    {
        private People mSelectedPeople;

        public People SeletedPeople
        {
            get { return mSelectedPeople; }
            set { mSelectedPeople = value; }
        }

        private PeopleCollection mPeoples = new PeopleCollection ( );

        public PeopleCollection Peoples
        {
            get { return mPeoples; }
        }

    }
}
