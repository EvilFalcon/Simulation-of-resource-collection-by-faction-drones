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
        [Header("Settings")]
        [SerializeField] private LineRenderer _line;
        [SerializeField] private float _lineWidth = 0.5f;

        private NavMeshAgent _agent;
        private GameEntity _entity;

        protected override void Subscribe(GameEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            _line.startWidth = _lineWidth;
            _line.endWidth = _lineWidth;
            _line.material = new Material(Shader.Find("Sprites/Default"))
            {
                color = Color.cyan
            };
            _entity = entity;
            _agent = entity.NavMeshAgent.Value;
            
            entity.SubscribeRay(OnRatEnable).AddTo(unsubscribe);
            entity.SubscribeRayRemoved(OnRatEnable).AddTo(unsubscribe);
        }

        private void OnRatEnable(GameEntity entity)
        {
            _line.enabled = entity.IsRay;
        }

        private void Update()
        {
            if (_entity.IsRay == false)
            {
                _line.enabled = false;
                return;
            }

            if (_agent.hasPath)
            {
                DrawPath(_agent.path);
            }
        }

        private void DrawPath(NavMeshPath path)
        {
            _line.positionCount = path.corners.Length;
            _line.SetPositions(path.corners);
        }
    }
}