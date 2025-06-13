// using Ecs.Utils;
// using Ecs.Utils.Groups;
// using InstallerGenerator.Attributes;
// using InstallerGenerator.Enums;
// using JCMG.EntitasRedux;
// using UnityEngine;
//
// namespace Ecs.Game.Systems.BlockLine
// {
//     [Install(ExecutionType.Game, ExecutionPriority.Normal, 700, nameof(EFeatures.Common))]
//     public class MoveBlocksDownSystem : IUpdateSystem
//     {
//         private readonly IGameGroupUtils _gameGroupUtils;
//         private readonly IBlocksLineParameters _blockLineParameters;
//
//         public MoveBlocksDownSystem(
//             IGameGroupUtils gameGroupUtils,
//             IBlocksLineParameters blockLineParameters
//         )
//         {
//             _gameGroupUtils = gameGroupUtils;
//             _blockLineParameters = blockLineParameters;
//         }
//
//         public void Update()
//         {
//             using var _ = _gameGroupUtils.GetLinesWithActiveCheck(out var lines, true, e => e.IsVisible);
//
//             if (lines.Count == 0)
//             {
//                 return;
//             }
//
//             var moveDelta = _blockLineParameters.BlockLinesMoveSpeed * Time.deltaTime;
//             
//             foreach (var line in lines)
//             {
//                 line.ReplaceMoveDown(line.MoveDown.Value - moveDelta);
//             }
//         }
//     }
// }
