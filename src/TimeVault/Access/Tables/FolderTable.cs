using TimeVault.Access.Models;
using Toolbox.Dapper.SQLite;

namespace TimeVault.Access.Tables
{
	internal class FolderTable() : DatabaseTable<Folder>("Folder")
	{
	}
}
