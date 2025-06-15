using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Views.Linkable.Views;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Ecs.Views.Linkable.Modules.Units
{
    public class AdvancedPathVisualizerModule : AObjectViewModule
    {
        private NavMeshAgent _agent;

        [Header("Animation")] [SerializeField] private bool _animatePath = true;
        private float _animationOffset;
        [SerializeField] private float _animationSpeed = 1f;
        private GameEntity _entity;
        [SerializeField] private float _heightOffset = 0.1f;

        [Header("Settings")] 
        [SerializeField] private LineRenderer _line;

        [SerializeField] private Material _lineMaterial;
        [SerializeField] private float _lineWidth = 0.2f;
        [SerializeField] private Color _pathColor = Color.cyan;
        private ITimeProvider _timeprovider;

        [Inject]
        private void Construct(ITimeProvider timeProvider)
        {
            _timeprovider = timeProvider;
        }

        protected override void Subscribe(GameEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            _entity = entity;
            _agent = entity.NavMeshAgent.Value;
            _line.material = _lineMaterial;
            _line.startColor = _line.endColor = _pathColor;
            _line.startWidth = _line.endWidth = _lineWidth;
            _line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        private void Update()
        {
            if (_entity.IsRay == false)
                return;
            
            if (_entity.NavMeshAgent.Value.isPathStale == false)
            {
                _line.enabled = false;
                return;
            }

            _line.enabled = true;
            UpdatePath();

            if (_animatePath)
            {
                AnimatePath();
            }
        }

        private void UpdatePath()
        {
            var path = _agent.path;
            _line.positionCount = path.corners.Length;

            for (int i = 0; i < path.corners.Length; i++)
            {
                var point = path.corners[i] + Vector3.up * _heightOffset;
                _line.SetPosition(i, point);
            }
        }

        private void AnimatePath()
        {
            _animationOffset += _timeprovider.DeltaTime * _animationSpeed;
            _line.material.mainTextureOffset = new Vector2(_animationOffset, 0);
        }
    }
}