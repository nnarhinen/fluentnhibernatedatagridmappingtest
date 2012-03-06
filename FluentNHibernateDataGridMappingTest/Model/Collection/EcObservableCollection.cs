using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;

namespace FluentNHibernateDataGridMappingTest.Model.Collection
{

	class EcObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
	{
		public EcObservableCollection() : base()
		{
			this.CollectionChanged += EcObservableCollection_CollectionChanged;		
		}

		public EcObservableCollection(IEnumerable<T> items) : base(items)
		{
			CollectionChanged += EcObservableCollection_CollectionChanged;
		}

		void EcObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add) {
				foreach (T item in e.NewItems)
					item.PropertyChanged += item_PropertyChanged;
			}
		}

		void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var args = new EcObservableCollectionItemChangedEventArgs<T>();
			args.Item = (T)sender;
			ItemChanged(this, args);
		}

		public event EcObservableCollectionItemChangedEventHandler<T> ItemChanged;
	}

	public delegate void EcObservableCollectionItemChangedEventHandler<T>(object sender, EcObservableCollectionItemChangedEventArgs<T> args);

	public class EcObservableCollectionItemChangedEventArgs<T> : EventArgs
	{
		public T Item { get; set; }
	}
}
