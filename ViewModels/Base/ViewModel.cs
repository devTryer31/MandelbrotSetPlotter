using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSetPlotter.Base.ViewModels {
	internal abstract class ViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propretyName = "")
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propretyName));

		protected virtual bool Set<T>(ref T source, T value, [CallerMemberName] string propretyName = "") {
			if (object.Equals(source, value))
				return false;

			source = value;
			OnPropertyChanged(propretyName);
			return true;
		}

	}
}
