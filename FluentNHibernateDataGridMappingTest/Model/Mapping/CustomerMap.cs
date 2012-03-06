using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace FluentNHibernateDataGridMappingTest.Model.Mapping
{
	public class CustomerMap : ClassMap<Customer>
	{
		public CustomerMap()
		{
			Id(x => x.Id);
			Map(x => x.Email);
			Map(x => x.Name);
			Map(x => x.Phone);
			HasMany(x => x.Addresses);
		}
	}
}
