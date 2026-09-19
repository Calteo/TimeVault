using TimeVault.Access.Models;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Tables
{
	internal class SelectionTable() : DatabaseTable<VaultDatabase, Selection>("Selection")
	{
		/*
		public IEnumerable<Selection> GetSelections()
		{
			bool excludedVault = false;

			foreach (var selection in Select())
			{				
			}
		}
		*/
	}
}
