using InstallerGenerator.Sort.Impls;
using UnityEngine;

namespace InstallerGenerator.Header
{
	public class OrderSortButtonDrawer : ASortButtonDrawer
	{
		private readonly OrderSortStrategy _sortStrategy;

		public OrderSortButtonDrawer(OrderSortStrategy sortStrategy, GUILayoutOption[] style)
			: base(sortStrategy, style)
		{
			_sortStrategy = sortStrategy;
		}

		protected override string GetButtonLabel() => "Order " + _sortStrategy.Name;
	}
}