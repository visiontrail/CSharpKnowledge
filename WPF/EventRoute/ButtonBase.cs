using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.EventRoute
{
    /// <summary>
    /// 路由事件的参数;
    /// ClickTime:当前点击时候的系统时间;
    /// </summary>
    public class ReportTimeEvtArgs : RoutedEventArgs
    {
        public ReportTimeEvtArgs(RoutedEvent re, object source) 
            : base(re, source){ }

        public DateTime ClickTime { get; set; }
    }

    /// <summary>
    /// 自定义一个Button按钮，这个按钮支持自定义的路由事件;
    /// 这个路由事件就是上报了一个当前按钮的点击时间;
    /// </summary>
    class ButtonTime : Button
    {
        // 一个路由事件静态实例;
        // 注意：路由事件的第一个名字需要和CLR事件的名称一致才行!!;
        public static readonly RoutedEvent ReportRoutedEvent = EventManager.RegisterRoutedEvent
            ("ReportTime", RoutingStrategy.Bubble, typeof(EventHandler<ReportTimeEvtArgs>), typeof(ButtonTime));

        // CLR事件包装;
        public event RoutedEventHandler ReportTime
        {
            add { this.AddHandler(ReportRoutedEvent, value); }
            remove { this.RemoveHandler(ReportRoutedEvent, value); }
        }

        // 当发生点击的时候，可以通过更新事件参数，将该自定义的路由事件参数传递出去;
        protected override void OnClick()
        {
            base.OnClick();

            ReportTimeEvtArgs args = new ReportTimeEvtArgs(ReportRoutedEvent, this);
            args.ClickTime = DateTime.Now;
            this.RaiseEvent(args);
        }
    }
}
