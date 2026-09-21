using TimeVault.Access.Models;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Tables
{
	internal class SelectionTable() : DatabaseTable<VaultDatabase, Selection>("Selection")
	{
		internal void Replace(List<Selection> selections)
		{
			using var connection = GetConnection();
			using var transaction = connection.BeginTransaction();

			// delete all previous selections
			DeleteWhere("", transaction); 

			// insert new selections
			foreach (var selection in selections)
				Insert(selection, transaction);

			transaction.Commit();
		}
	}
}
