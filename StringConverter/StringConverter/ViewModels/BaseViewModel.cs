using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StringConverter.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private WeakReference<INavigation> _navigationReference;
        private bool isBusy = false;

        public INavigation Navigation
        {
            get
            {
                INavigation _navigation = null;
                _navigationReference.TryGetTarget(out _navigation);
                return _navigation;
            }
            set
            {
                _navigationReference = new WeakReference<INavigation>(value);
            }
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public virtual Task OnLoadAsync() { return Task.CompletedTask; }
        public virtual Task UnLoadAsync() { return Task.CompletedTask; }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
