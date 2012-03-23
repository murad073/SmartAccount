using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMTest
{
    public class Factory
    {
        public static IMainWindow CreateMainWindow(MainWindowClassType classType)
        {
            IMainWindow instance = default(IMainWindow);
            switch (classType)
            {
                case MainWindowClassType.DummyClass:
                    instance= new ViewModel();
                    break;
                case MainWindowClassType.FunctionalClass:
                    instance= new ViewModel();
                    break;
                default:
                    break;
            }
            return instance;
        }

        //public static ILedger CreateLedget()
        //{
        //}

        public enum MainWindowClassType
        {
            DummyClass,
            FunctionalClass
        }


    }
}
