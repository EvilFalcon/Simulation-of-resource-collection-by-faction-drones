// using Db.BlocksLine;
// using Db.PlayerFigure;
// using Ecs.Utils;
// using Ecs.Utils.Groups;
// using InstallerGenerator.Attributes;
// using InstallerGenerator.Enums;
// using JCMG.EntitasRedux;
// using UnityEngine;
//
// namespace Ecs.Game.Systems.MovableFigure
// {
//     [Install(ExecutionType.Game, ExecutionPriority.Normal, 700, nameof(EFeatures.Common))]
//     public class MoveMovableFigureSystem : IUpdateSystem
//     {
//         private readonly IGameGroupUtils _gameGroupUtils;
//         private readonly IMovableFigureParameters _movableFigureParameters;
//         private readonly IBlocksLineParameters _blocksLineParameters;
//         private readonly GameContext _game;
//
//         public MoveMovableFigureSystem(
//             IGameGroupUtils gameGroupUtils,
//             IMovableFigureParameters movableFigureParameters,
//             IBlocksLineParameters blocksLineParameters,
//             GameContext game
//         )
//         {
//             _gameGroupUtils = gameGroupUtils;
//             _movableFigureParameters = movableFigureParameters;
//             _blocksLineParameters = blocksLineParameters;
//             _game = game;
//         }
//         
//         public void Update()
//         {
//             using var _ = _gameGroupUtils.GetMovableFigureWithActiveCheck(out var movableFigures, true);
//
//             foreach (var movableFigure in movableFigures)
//             {
//                 var newPosition = movableFigure.Position.Value;
//                 newPosition.y += Time.deltaTime * _movableFigureParameters.FigureMoveSpeed;
//                 
//                 movableFigure.ReplacePosition(newPosition);
//                 
//                 CheckFigureReachedDestination(movableFigure);
//             }
//         }
//         
//         private void CheckFigureReachedDestination(GameEntity movableFigure)
//         {
//             var targetLine = _game.GetEntityWithUid(movableFigure.FiguresTargetLinesUid.Value);
//             
//             if (targetLine.HasMoveDown)
//             {
//                 if (movableFigure.Position.Value.y + movableFigure.OffsetToTargetLine.Value >= 
//                     targetLine.MoveDown.Value &&
//                     movableFigure.IsFigureReachedDestination == false) 
//                 {
//                     movableFigure.IsFigureReachedDestination = true;
//                 }
//             }
//             else
//             {
//                 if (movableFigure.Position.Value.y + movableFigure.OffsetToTargetLine.Value >= 
//                      targetLine.DelayActivationDistance.Value + _blocksLineParameters.BlockLineStartYPosition && 
//                     movableFigure.IsFigureReachedDestination == false)
//                 {
//                     movableFigure.IsFigureReachedDestination = true;
//                 }
//             }
//         }
//     }
// }