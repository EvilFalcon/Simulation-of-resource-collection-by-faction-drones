using System;

namespace UniRx.Operators
{
	public class SingleOperator<T> : OperatorObservableBase<T>
	{
		private readonly IObservable<T> _source;

		public SingleOperator(IObservable<T> source) : base(source.IsRequiredSubscribeOnCurrentThread())
		{
			_source = source;
		}

		protected override IDisposable SubscribeCore(IObserver<T> observer, IDisposable cancel)
		{
			return _source.Subscribe(new SingleObservable(observer, cancel));
		}

		private class SingleObservable : OperatorObserverBase<T, T>
		{
			public SingleObservable(IObserver<T> observer, IDisposable cancel) : base(observer, cancel)
			{
			}

			public override void OnNext(T value)
			{
				observer.OnNext(value);

				try { observer.OnCompleted(); }
				finally { Dispose(); }
			}

			public override void OnError(Exception error)
			{
				try { observer.OnError(error); }
				finally { Dispose(); }
			}

			public override void OnCompleted()
			{
				
			}
		}
	}
}