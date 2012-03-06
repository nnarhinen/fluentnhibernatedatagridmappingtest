using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernateDataGridMappingTest.Model
{
	public class CustomerAddress
	{
		public virtual int Id { get; set; }
		public virtual string Street { get; set; }
		public virtual string Zip { get; set; }
		public virtual string City { get; set; }
		public virtual string Country { get; set; }

		public virtual Customer Customer { get; set; }
	}
}
