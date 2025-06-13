using System;
using System.Collections.Generic;
using Ecs.Core.Interfaces;
using JCMG.EntitasRedux;
using PdUtils.Interfaces;
using Zenject;

namespace Ecs.Core.Bootstrap
{
	public class Bootstrap : IBootstrap, ITickable, ILateTickable, IFixedTickable, ILateFixed,
		IGuiRenderable, IGizmoRenderable
	{
		private readonly Contexts _contexts;
		private readonly CustomFeature _feature;
		private readonly List<IStartable> _startables;
		private readonly List<IResetable> _resetables;
		private readonly List<ILateSystem> _late = new();
		private readonly List<IFixedSystem> _fixed = new();
		private readonly List<ILateFixedSystem> _lateFixed = new();
		private readonly List<IGuiSystem> _gui = new();
		private readonly List<IGizmoSystem> _gizmo = new();
		private bool _isInitialized;
		private bool _isPaused;

		public Bootstrap(
			[InjectLocal] CustomFeature feature,
			[InjectLocal] Contexts contexts,
			[InjectLocal] List<ISystem> systems,
			[InjectLocal] List<IStartable> startables,
			[InjectLocal] List<IResetable> resetables
		)
		{
			_contexts = contexts;
			_startables = startables;
			_resetables = resetables;
			_feature = feature;
			for (var i = 0; i < systems.Count; i++)
			{
				var system = systems[i];
				_feature.Add(system);
				if (system is ILateSystem late)
					_late.Add(late);
				if (system is IFixedSystem @fixed)
					_fixed.Add(@fixed);
				if (system is ILateFixedSystem lateFixed)
					_lateFixed.Add(lateFixed);
				if (system is IGuiSystem gui)
					_gui.Add(gui);
				if (system is IGizmoSystem gizmo)
					_gizmo.Add(gizmo);
			}
		}

		public void Initialize()
		{
			if (_isInitialized)
				throw new Exception("[MainBootstrap] Bootstrap already is initialized");

			if (_startables != null)
				foreach (var pool in _startables)
					pool.Start();
			_feature.Initialize();
			_isInitialized = true;
		}

		public void Tick()
		{
			if (_isPaused)
				return;

			_feature.Update();
		}

		public void FixedTick()
		{
			if (_isPaused)
				return;

			for (var i = 0; i < _fixed.Count; i++)
				_fixed[i].Fixed();
		}

		public void LateFixed()
		{
			if (_isPaused)
				return;
			for (var i = 0; i < _lateFixed.Count; i++)
				_lateFixed[i].LateFixed();
		}

		public void LateTick()
		{
			if (_isPaused)
				return;

			for (var i = 0; i < _late.Count; i++)
				_late[i].Late();

			_feature.Cleanup();
		}

		public void GuiRender()
		{
			if (_isPaused)
				return;

			for (var i = 0; i < _gui.Count; i++)
				_gui[i].Gui();
		}

		public void GizmoRender()
		{
			if (_isPaused)
				return;

			for (var i = 0; i < _gizmo.Count; i++)
				_gizmo[i].Gizmo();
		}

		public void Pause(bool isPaused)
		{
			_isPaused = isPaused;
		}

		public void Reset()
		{
			Pause(true);

			_feature.Deactivate();
			foreach (var context in _contexts.AllContexts)
			{
				context.DestroyAllEntities();
				context.ResetCreationIndex();
			}

			foreach (var resetable in _resetables)
				resetable.Reset();

			_feature.Activate();
			_isInitialized = false;
			Initialize();

			Pause(false);
		}

		public void Dispose()
		{
			_feature.Deactivate();
			_contexts.Reset();
		}
	}
}