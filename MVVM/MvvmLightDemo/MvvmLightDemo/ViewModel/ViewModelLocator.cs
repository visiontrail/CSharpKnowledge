/*
 * Locator类型起到的作用;
 * 
 * 
  1:在App.xaml添加了全局资源:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MvvmLightDemo" x:Key="Locator" />
  </Application.Resources>
  
  2:在前端，将MainWindow的DataContext绑定给了Locator:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  3:在自己的构造函数中，注册了所有需要MvvmLight控制的VM层的类型;
  SimpleIoc.Default.Register<MainViewModel>();

  4:用一个属性将View和ViewModel关联起来
  当前这个类，使用的是Main这个属性;
  
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace MvvmLightDemo.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// 这个类型的用处就是包含了所有的View和ViewModel之间的关联，使得View和ViewModel不再直接引用;
    /// 
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // 在Visual Studio中设计预览Xaml文件的时候，在这里注册会起作用;
                SimpleIoc.Default.Register<MainViewModel>();
            }
            else
            {
                // 在运行时，在这里注册会起作用;
                SimpleIoc.Default.Register<MainViewModel>();
            }

            // 更多的时候，默认在这里注册就可以了;
            SimpleIoc.Default.Register<MainViewModel>();
            
        }

        /// <summary>
        /// 这里对应的是View中DataContext需要的依赖属性;
        /// 就是这个Main属性，关联起了View和ViewModel;
        /// DataContext="{Binding Source={StaticResource Locator}, Path=Main}
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}