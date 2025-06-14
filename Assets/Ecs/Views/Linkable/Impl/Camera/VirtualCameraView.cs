using System.Collections.Generic;
using Ecs.Utils.Camera;
using Game.Models.Camera;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Unity.Cinemachine;
using UnityEngine;
using Utils.Drawers.Key;
using Inject = Zenject.InjectAttribute;

namespace Ecs.Views.Linkable.Impl.Camera
{
	public class VirtualCameraView : ObjectView
	{
		[SerializeField] 
		[KeyValue(nameof(CameraTypeData.VirtualCameraType))] 
		private List<CameraTypeData> _virtualCameras;

		private IPlayerCameraHolder _cameraHolder;
		
		[Inject]
		private void Construct(IPlayerCameraHolder playerCameraHolder)
		{
			_cameraHolder = playerCameraHolder;
		}

		protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
		{
			base.Subscribe(entity, unsubscribe);
			
			var camerasDictionary = GetCamerasDictionary();
			_cameraHolder.Init(camerasDictionary);
		}
		
		private IReadOnlyDictionary<EVirtualCameraType, CinemachineVirtualCamera> GetCamerasDictionary()
		{
			var camerasDictionary = new Dictionary<EVirtualCameraType, CinemachineVirtualCamera>();
		
			foreach (var cameraTypeData in _virtualCameras)
			{
				camerasDictionary.Add(cameraTypeData.VirtualCameraType, cameraTypeData.VirtualCamera);
			}
		
			return camerasDictionary;
		}
	}
}