using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace FluentNHibernateDataGridMappingTest.Core
{
	class Database
	{
		public static ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile("test.db"))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<App>())
				.ExposeConfiguration(BuildDataBase)
				.BuildSessionFactory();
		}

		private static void BuildDataBase(Configuration conf)
		{
			conf.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
			if (!File.Exists("test.db"))
				new SchemaExport(conf).Create(false, true);
		}

		internal static ISession GetSession(ISessionFactory sessionFactory)
		{
			try {
				return sessionFactory.GetCurrentSession();
			}
			catch (HibernateException) { }
			ISession session = sessionFactory.OpenSession();
			session.FlushMode = FlushMode.Always;
			return session;
		}
	}
}
