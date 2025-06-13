// using Db.BlocksLine;
// using Ecs.Utils;
// using Ecs.Utils.Groups;
// using Game.Services.AttachFigureToLinesService;
// using Game.Services.CreateBlockLineService;
// using InstallerGenerator.Attributes;
// using InstallerGenerator.Enums;
// using JCMG.EntitasRedux;
// using UnityEngine;
//
// namespace Ecs.Game.Systems.BlockLine
// {
//     [Install(ExecutionType.Game, ExecutionPriority.Normal, 800, nameof(EFeatures.Common))]
//     public class BlockLinesAppearanceSystem : IUpdateSystem
//     {
//         private readonly IGameGroupUtils _gameGroupUtils;
//       
//         private readonly ICreateBlockLineService _createBlockLineService;
//         private readonly IAttachFigureToLinesService _attachFigureToLinesService;
//
//         public BlockLinesAppearanceSystem(
//             IGameGroupUtils gameGroupUtils,
//          
//             ICreateBlockLineService createBlockLineService,
//             IAttachFigureToLinesService attachFigureToLinesService
//         )
//         {
//             _gameGroupUtils = gameGroupUtils;
//           
//             _createBlockLineService = createBlockLineService;
//             _attachFigureToLinesService = attachFigureToLinesService;
//         }
//
//         public void Update()
//         {
//             using var _ = _gameGroupUtils.GetLinesWithActiveCheck(out var lines, true, e => e.HasDelayActivationDistance);
//
//             if (lines.Count == 0)
//             {
//              
//                 
//                 return;
//             }
//
//             foreach (var line in lines)
//             {
//               
//
//                
//                 {
//                     var offset = Mathf.Abs(leftDistance);
//                     
//                     line.RemoveDelayActivationDistance();
//                     line.IsVisible = true;
//                   
//                     
//                     _attachFigureToLinesService.OverrideFiguresTargetLinesUid();
//
//                     if (lines.Count == 1)
//                     {
//                         _createBlockLineService.CreateFiguresOnLines(offset);
//                     }
//                 }
//                 else
//                 {
//                     line.ReplaceDelayActivationDistance(leftDistance);
//                 }
//             }
//         }
//     }
// }
