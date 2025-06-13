using System;
using Zenject;

namespace ZenjectUtil.Extensions
{
	public static class ZenjectBindFactoryExtensions
	{
		public static void BindFactoryInterface<TContract, TFactory>(this DiContainer container)
			where TFactory : IFactory<TContract>
		{
			container.Bind<IFactory<TContract>>().To<TFactory>().AsSingle();
		}

		public static void BindFactoryInterface<TParam, TContract, TFactory>(this DiContainer container)
			where TFactory : IFactory<TParam, TContract>
		{
			container.Bind<IFactory<TParam, TContract>>().To<TFactory>().AsSingle();
		}

		public static void BindFactoryInterface<TParam1, TParam2, TContract, TFactory>(this DiContainer container)
			where TFactory : IFactory<TParam1, TParam2, TContract>
		{
			container.Bind<IFactory<TParam1, TParam2, TContract>>().To<TFactory>().AsSingle();
		}

		public static void BindFactoryInterface<TParam1, TParam2, TParam3, TContract, TFactory>(
			this DiContainer container
		)
			where TFactory : IFactory<TParam1, TParam2, TParam3, TContract>
		{
			container.Bind<IFactory<TParam1, TParam2, TParam3, TContract>>().To<TFactory>().AsSingle();
		}

		public static ArgConditionCopyNonLazyBinder FromFactory<TContract, TFactory>(
			this FactoryToChoiceBinder<TContract> factoryToChoiceBinder,
			Action<ConcreteIdArgConditionCopyNonLazyBinder> factoryBinder = null
		)
			where TFactory : IFactory<TContract>
			=> factoryToChoiceBinder.FromIFactory(x =>
			{
				var conditionCopyNonLazyBinder = x.To<TFactory>().AsCached();
				factoryBinder?.Invoke(conditionCopyNonLazyBinder);
			});
	}
}