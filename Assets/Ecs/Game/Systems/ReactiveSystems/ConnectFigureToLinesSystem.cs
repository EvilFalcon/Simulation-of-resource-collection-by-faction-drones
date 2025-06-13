// using System.Collections.Generic;
// using Db.BlocksLine;
// using Ecs.Utils;
// using Game.Services.AttachFigureToLinesService;
// using Game.Services.BlockLineService;
// using Game.Services.GameStateService;
// using Game.Services.MoveBlockLinesService;
// using InstallerGenerator.Attributes;
// using InstallerGenerator.Enums;
// using JCMG.EntitasRedux;
//
// namespace Ecs.Game.Systems.MovableFigure
// {
//     [Install(ExecutionType.Game, ExecutionPriority.Normal, 1300, nameof(EFeatures.Common))]
//     public class ConnectFigureToLinesSystem : ReactiveSystem<GameEntity>
//     {
//         private readonly IAttachFigureToLinesService _attachFigureToLinesService;
//         private readonly IMoveBlockLinesService _moveBlockLinesService;
//         private readonly GameContext _game;
//         private readonly IBlocksLineParameters _blocksLineParameters;
//         private readonly IBlockLineService _blockLineService;
//         private readonly IGameStateService _gameStateService;
//         
//         public ConnectFigureToLinesSystem(
//             GameContext game,
//             IAttachFigureToLinesService attachFigureToLinesService,
//             IMoveBlockLinesService moveBlockLinesService,
//             IBlocksLineParameters blocksLineParameters,
//             IBlockLineService blockLineService,
//             IGameStateService gameStateService
//         ) : base(game)
//         {
//             _attachFigureToLinesService = attachFigureToLinesService;
//             _moveBlockLinesService = moveBlockLinesService;
//             _blocksLineParameters = blocksLineParameters;
//             _blockLineService = blockLineService;
//             _gameStateService = gameStateService;
//             _game = game;
//         }
//
//         protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
//             context.CreateCollector(GameMatcher.FigureReachedDestination);
//
//         protected override bool Filter(GameEntity entity) => !entity.IsDestroyed;
//
//         protected override void Execute(IEnumerable<GameEntity> entities)
//         {
//             foreach (var movableFigure in entities)
//             {
//                 var targetLine = _game.GetEntityWithUid(movableFigure.FiguresTargetLinesUid.Value);
//                 
//                 if (targetLine.IsWasInactiveWhenTargeted)
//                 {
//                     _attachFigureToLinesService.AttachFigureWithInactiveTargetToLines(movableFigure);
//                 }
//                 else
//                 {
//                     _attachFigureToLinesService.AttachFigureToLines(movableFigure);
//                 }
//             }
//             
//             _game.ReplaceStepsToNewLine(_game.StepsToNewLine.Value - 1);
//             
//             if (_game.StepsToNewLine.Value == 0)
//             {
//                 _game.ReplaceStepsToNewLine(_blocksLineParameters.StepsToNewLine);
//                 
//                 _moveBlockLinesService.MoveBlockLines();
//             }
//             
//             if (_blockLineService.CheckLinesReachedBottomBorder())
//             {
//                 _gameStateService.LoseGame();
//             }
//         }
//     }
// }