using InstallerGenerator.Sort.Impls;
using UnityEngine;

namespace InstallerGenerator.Header
{
	public class NameSortButtonDrawer : ASortButtonDrawer
	{
		private readonly NameSortStrategy _sortStrategy;

		public NameSortButtonDrawer(NameSortStrategy sortStrategy, GUILayoutOption[] style)
			: base(sortStrategy, style)
		{
			_sortStrategy = sortStrategy;
		}

		protected override string GetButtonLabel() => "Name " + _sortStrategy.Name;
	}
}