using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShoppingList.ViewModels
{
    public class ObservableObject : INotifyPropertyChanged
    {
		private Dictionary<string, IList<string>> _dependencies = new Dictionary<string, IList<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

		public ObservableObject()
		{
			RegisterDependencies();
		}

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

			if (_dependencies.ContainsKey(propertyName))
				foreach (var dependency in _dependencies[propertyName])
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(dependency));
        }

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value))
                return false;

            property = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

		protected void RegisterDependency(string sourceProperty, string dependentProperty)
		{
			if (!_dependencies.ContainsKey(sourceProperty))
				_dependencies.Add(sourceProperty, new List<string>());

			_dependencies[sourceProperty].Add(dependentProperty);
		}

		protected virtual void RegisterDependencies()
		{

		}
    }
}
