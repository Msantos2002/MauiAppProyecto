using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiAppProyecto.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
        }

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        string tittle;

        public bool IsNotBusy => !IsBusy;


        partial void OnIsBusyChanged(bool value)
        {
 
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }
}
