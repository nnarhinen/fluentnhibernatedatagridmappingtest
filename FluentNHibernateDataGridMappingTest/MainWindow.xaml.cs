using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NHibernate;
using FluentNHibernateDataGridMappingTest.Core;
using FluentNHibernateDataGridMappingTest.Model;
using System.Collections.ObjectModel;
using FluentNHibernateDataGridMappingTest.Model.Collection;
using System.Collections.Specialized;

namespace FluentNHibernateDataGridMappingTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ISessionFactory sessionFactory;

		private EcObservableCollection<CustomerView> customers;
		public MainWindow()
		{
			InitializeComponent();
			sessionFactory = Database.CreateSessionFactory();
			using (var session = Database.GetSession(sessionFactory)) {
				var criteria = session.CreateCriteria<Customer>();
				customers = new EcObservableCollection<CustomerView>(criteria.List<Customer>().Select(c => new CustomerView(c)));
			}
			customers.CollectionChanged += customers_CollectionChanged;
			customers.ItemChanged += customers_ItemChanged;
			dataGrid1.ItemsSource = customers;
		}

		void customers_ItemChanged(object sender, EcObservableCollectionItemChangedEventArgs<CustomerView> args)
		{
			using (var session = Database.GetSession(sessionFactory)) {
				session.SaveOrUpdate(args.Item.InnerCustomer);
				session.Flush();
			}
		}

		void customers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			using (var session = Database.GetSession(sessionFactory)) {
				switch (e.Action) {
					case NotifyCollectionChangedAction.Remove:
						foreach (CustomerView c in e.OldItems) {
							session.Delete(c.InnerCustomer);
							session.Flush();
						}
						break;
					case NotifyCollectionChangedAction.Add:
						foreach (CustomerView c in e.NewItems)
							session.Save(c.InnerCustomer);
						break;
					default:
						foreach (CustomerView c in e.OldItems)
							session.SaveOrUpdate(c.InnerCustomer);
						break;
				}
			}
				
		}
	}
}
