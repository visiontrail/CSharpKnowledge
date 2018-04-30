using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace DataGridBinding
{
    public class PeopleDataGrid : DataGrid
    {
        public PeopleDataGrid ( )
            : base ( )
        {
            AutoGenerateColumns = false;
            CanUserAddRows = false;
        }

        // 这个控件的一个自定义属性;
        private PeoplesViewModel mDataSource;
        public PeoplesViewModel DataSource
        {
            get { return mDataSource; }
            set
            {
                mDataSource = value;
            }
        }

        /// <summary>
        /// 定义了一个名为DataSourc的控件依赖属性;
        /// 这个依赖属性用于显示peopoleDataGrid的内容;
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register ( "DataSource", typeof ( PeoplesViewModel ), typeof ( PeopleDataGrid ),
            new FrameworkPropertyMetadata ( new PropertyChangedCallback ( OnDataSourcePeopertyChanged ) ) );

        private static void OnDataSourcePeopertyChanged ( DependencyObject obj, DependencyPropertyChangedEventArgs args )
        {
            PeopleDataGrid peopleDataGrid = ( PeopleDataGrid ) obj;

            if ( args.NewValue is PeoplesViewModel )
            {
                if ( peopleDataGrid != null )
                {
                    PeoplesViewModel peoplesViewModel = args.NewValue as PeoplesViewModel;
                    peopleDataGrid.ItemsSource = peoplesViewModel.Peoples;

                    peopleDataGrid.Columns.Clear ( );
                    DataGridTextColumn nameColumn = new DataGridTextColumn ( );
                    nameColumn.Header = "Name";
                    nameColumn.Binding = new Binding ( "Name" );

                    DataGridTextColumn ageColumn = new DataGridTextColumn ( );
                    ageColumn.Header = "Age";
                    ageColumn.Binding = new Binding ( "Person.Age" );

                    DataGridTextColumn sexColumn = new DataGridTextColumn ( );
                    sexColumn.Header = "Sex";
                    sexColumn.Binding = new Binding ( "Person.Sex" );

                    peopleDataGrid.Columns.Add ( nameColumn );
                    peopleDataGrid.Columns.Add ( ageColumn );
                    peopleDataGrid.Columns.Add ( sexColumn );
                }
            }
            
        }
    }
}
