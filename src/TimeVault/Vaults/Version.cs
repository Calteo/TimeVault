namespace TimeVault.Vaults
{
	internal class Version
	{
		public int Id { get; set; }
		public DateTime CreateAt { get; set; } = DateTime.MinValue;
		public string Comment { get; set; } = "";
	}
}
