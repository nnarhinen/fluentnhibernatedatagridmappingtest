using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FluentNHibernateDataGridMappingTest.Model
{
	public class Customer
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Email { get; set; }
		public virtual string Phone { get; set; }

		public virtual IList<CustomerAddress> Addresses { get; set; }

		public Customer()
		{
			Addresses = new List<CustomerAddress>();
		}

		public virtual void AddAddress(CustomerAddress address)
		{
			address.Customer = this;
			Addresses.Add(address);
		}
	}

	public class CustomerView : INotifyPropertyChanged
	{
		private Customer _customer;

		public Customer InnerCustomer { get { return _customer; } }

		public CustomerView()
		{
			_customer = new Customer();
		}

		public CustomerView(Customer customer)
		{
			_customer = customer;
		}

		public virtual int Id
		{
			get { return _customer.Id; }
			set
			{
				_customer.Id = value;
				NotifyPropertyChanged("Id");
			}
		}
		public virtual string Name
		{
			get { return _customer.Name; }
			set
			{
				_customer.Name = value;
				NotifyPropertyChanged("Name");
			}
		}
		public virtual string Email
		{
			get { return _customer.Email; }
			set
			{
				_customer.Email = value;
				NotifyPropertyChanged("Email");
			}
		}
		public virtual string Phone
		{
			get { return _customer.Phone; }
			set
			{
				_customer.Phone = value;
				NotifyPropertyChanged("Phone");
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propertyChanged)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyChanged));
		}

		#endregion
	}

}
