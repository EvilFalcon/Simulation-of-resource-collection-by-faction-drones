using Core.Impl;
using Game.Services.SceneLoading;
using Game.Services.SceneLoading.Impls;
using PdUtils.DateTimeService;
using PdUtils.DateTimeService.Impl;
using PdUtils.FirstStartService;
using PdUtils.FirstStartService.Impl;
using PdUtils.GeoLocation;
using PdUtils.GeoLocation.Impl;
using PdUtils.IntervalTimeManager.Impl;
using PdUtils.MailService.Impl;
using PdUtils.PlayerPrefs;
using PdUtils.PlayerPrefs.Impl;
using PdUtils.RandomProvider.Impl;
using PdUtils.SceneLoadingProcessor.Impls;
using PdUtils.ScheduledExecutorService;
using PdUtils.ScheduledExecutorService.Impl;
using PdUtils.Vibration.Impl;
using PdUtils.Web.Impl;
using Project;
using UniRx;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Installers.Project
{
	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			MainThreadDispatcher.Initialize();
#if UNITY_EDITOR
			Application.targetFrameRate = -1;
#else
			Application.targetFrameRate = 60;
#endif

			Container.BindInterfacesTo<UnityRandomProvider>().AsSingle();
			Container.BindInterfacesTo<LoadingProcessor>().AsSingle();
			Container.BindInterfacesTo<GameSceneProvider>().AsSingle();

			Container.BindSubstituteInterfacesTo<ISceneLoader, SceneLoader>().AsSingle();

			Container.BindFromSubstitute<IPlayerPrefsManager, PersistancePlayerPrefsManager>().AsSingle();

			// Container.BindFromSubstitute<IAppVersionService, AppVersionService>().AsSingle();

			Container.BindFromSubstitute<IDateTimeService, DateTimeService>().AsSingle();

			Container.BindInterfacesAndSelfTo<IntervalTimeManager>().AsTransient();

			Container.BindFromSubstitute<IFirstStartService, FirstStartService>().AsSingle();

			Container.BindInterfacesAndSelfTo<VibrationService>().AsSingle();

			SignalBusInstaller.Install(Container);

			Container.BindInterfacesTo<GeoLocationService>().AsSingle();

			Container.Bind<IScheduledExecutorService>().To<ScheduledExecutorService>().AsSingle();

			Container.BindInterfacesAndSelfTo<WebRequester<LocationVo>>().AsTransient();
			Container.BindInterfacesAndSelfTo<WebRequester<string>>().AsTransient();
			Container.Bind<GeoLocationService.Settings>().FromInstance(new GeoLocationService.Settings(24L))
				.AsSingle().WhenInjectedInto<GeoLocationService>();
			
			Container.BindInterfacesAndSelfTo<MailService>().AsSingle();
			Container.BindInterfacesTo<ProjectWindowManager>().AsSingle().NonLazy();
			
		}

		public override void Start()
		{
			Debug.Log("device ID = " + SystemInfo.deviceUniqueIdentifier);
		}
	}
}