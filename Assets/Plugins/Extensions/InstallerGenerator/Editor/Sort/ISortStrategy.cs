using System.Collections.Generic;
using InstallerGenerator.Models;

namespace InstallerGenerator.Sort
{
	public interface ISortStrategy
	{
		string Name { get; }

		void Next();

		void Reset();

		void Sort(List<AttributeRecord> records);
	}
}