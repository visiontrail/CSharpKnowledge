using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace DataGridBinding
{
    /// <summary>
    /// 显示在前台表1的VM层;
    /// </summary>
    public class PeoplesViewModel
    {
        // 当前被选择的数据;
        private People mSelectedPeople;
        public People SeletedPeople
        {
            get { return mSelectedPeople; }
            set { mSelectedPeople = value; }
        }

        // 数据集合;
        private PeopleCollection mPeoples = new PeopleCollection ( );
        public PeopleCollection Peoples
        {
            get { return mPeoples; }
        }

    }
}
