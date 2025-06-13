// using System.Collections.Generic;
// using Db.PlayerFigure;
// using Ecs.Utils;
// using InstallerGenerator.Attributes;
// using InstallerGenerator.Enums;
// using JCMG.EntitasRedux;
// using UnityEngine;
//
// namespace Ecs.Game.Systems.MovableFigure
// {
//     [Install(ExecutionType.Game, ExecutionPriority.Normal, 1100, nameof(EFeatures.Common))]
//     public class ChangeFigureScaleSystem : ReactiveSystem<GameEntity>
//     {
//         private readonly IMovableFigureParameters _movableFigureParameters;
//         
//         public ChangeFigureScaleSystem(
//             IContext<GameEntity> context,
//             IMovableFigureParameters movableFigureParameters
//         ) : base(context)
//         {
//             _movableFigureParameters = movableFigureParameters;
//         }
//
//         protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
//             context.CreateCollector(GameMatcher.MovableFigureDrags);
//
//         protected override bool Filter(GameEntity entity) => !entity.IsDestroyed;
//
//         protected override void Execute(IEnumerable<GameEntity> entities)
//         {
//             foreach (var entity in entities)
//             {
//                 entity.ReplaceScale(Vector3.one * _movableFigureParameters.FigureDragScale);
//             }
//         }
//     }
// }