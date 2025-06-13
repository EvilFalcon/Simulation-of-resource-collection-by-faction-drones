namespace Ecs.Utils
{
	public readonly struct VirtualBalanceSource
	{
		public static readonly VirtualBalanceSource Empty = new VirtualBalanceSource();

		public readonly string Name;
		public readonly string Argument;

		public VirtualBalanceSource(string name)
		{
			Name = name;
			Argument = null;
		}

		public VirtualBalanceSource(string name, string argument)
		{
			Name = name;
			Argument = argument;
		}
	}
}