using InstallerGenerator.Models;

namespace InstallerGenerator.Sort.Impls
{
	public class OrderSortStrategy : AAscendingOrDescendingSortStrategy<int>
	{
		protected override int GetValue(AttributeRecord record) => record.Attribute.Order;
	}
}