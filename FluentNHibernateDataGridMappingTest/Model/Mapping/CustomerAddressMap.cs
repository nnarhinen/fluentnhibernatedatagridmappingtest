using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace FluentNHibernateDataGridMappingTest.Model.Mapping
{
	public class CustomerAddressMap : ClassMap<CustomerAddress>
	{
		public CustomerAddressMap()
		{
			Id(x => x.Id);
			Map(x => x.City);
			Map(x => x.Country);
			Map(x => x.Street);
			Map(x => x.Zip);
			References(x => x.Customer);
		}
	}
}
