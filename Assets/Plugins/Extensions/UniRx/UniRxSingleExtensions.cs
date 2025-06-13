using System;
using UniRx.Operators;

namespace UniRx
{
	public static class UniRxSingleExtensions
	{
		public static IObservable<T> ToSingle<T>(this IObservable<T> observable) => new SingleOperator<T>(observable);
	}
}