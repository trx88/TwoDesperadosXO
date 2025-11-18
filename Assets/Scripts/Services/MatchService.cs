using System;
using System.Collections.Generic;
using System.Linq;
using Repository.DataRepositories.Abstraction;
using Repository.DataRepositories.RepositoryFactories.Factories;
using UI.Models.Game;
using UI.Models.Stats;

namespace Services
{
    public class MatchService
    {
        private readonly CounterService _counterService;
        private readonly IRepository<GameModel> _gameModelRepository;
        private readonly IRepository<StatsModel> _statsModelRepository;
        private GameModel _gameData;
        private StatsModel _statsModel;
        
        private float _totalMatchTime;
        
        private readonly int[][] _winLines =
        {
            new []{0,1,2}, new []{3,4,5}, new []{6,7,8}, // rows
            new []{0,3,6}, new []{1,4,7}, new []{2,5,8}, // cols
            new []{0,4,8}, new []{2,4,6}                 // diagonals
        };
        
        public MatchService(
            InMemoryRepositoryFactory inMemoryRepositoryFactory, 
            PlayerPrefsRepositoryFactory playerPrefsRepositoryFactory,
            CounterService counterService)
        {
            _counterService = counterService;
            
            _gameModelRepository = inMemoryRepositoryFactory.RepositoryOf<GameModel>();
            _statsModelRepository = playerPrefsRepositoryFactory.RepositoryOf<StatsModel>();
            
            // _gameData = _gameModelRepository.Get(_ => true).Single();
            // _statsModel = _statsModelRepository.Get(_ => true).Single();
        }

        public void HandleMove(int index)
        {
            if (_gameData.Board[index] != 0)
            {
                return;
            }

            if (_gameData.CurrentPlayer == 1)
            {
                _gameData.PlayerOneMoves++;
            }
            else
            {
                _gameData.PlayerTwoMoves++;
            }
            
            _gameData.Board[index] = _gameData.CurrentPlayer;
            _gameData.CurrentPlayer = _gameData.CurrentPlayer == 1 ? 2 : 1;
            _gameData.MatchResult = CheckResult(_gameData.Board);
            if (_gameData.MatchResult != GameOutcome.None)
            {
                EndMatch(_gameData.MatchResult);
            }
            _gameModelRepository.Update(_gameData);
        }
        
        private GameOutcome CheckResult(List<int> board)
        {
            foreach (var line in _winLines)
            {
                int a = line[0], b = line[1], c = line[2];
                int cell = board[a];

                if (cell != 0 && cell == board[b] && cell == board[c])
                {
                    return cell == 1 ? GameOutcome.WinX : GameOutcome.WinO;
                }
            }
            
            bool noEmpty = true;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == 0)
                {
                    noEmpty = false;
                    break;
                }
            }

            if (noEmpty)
            {
                return GameOutcome.Draw;
            }

            return GameOutcome.None;
        }
        
        public void StartMatch()
        {
            ResetCacheData();
            
            //Get fresh data
            _gameData = _gameModelRepository.Get(_ => true).Single();
            _statsModel = _statsModelRepository.Get(_ => true).Single();
            
            _counterService.StartCounter(OnTimerTick);
        }

        public void EndMatch(GameOutcome gameOutcome)
        {
            _counterService.StopCounter();
            
            float totalPlayedTime = _statsModel.NumberOfMatches * _statsModel.AverageMatchTime;
            _statsModel.NumberOfMatches++;
            float newAverageTime = (float)(totalPlayedTime + _gameData.MatchTime) / _statsModel.NumberOfMatches;
            _statsModel.AverageMatchTime = newAverageTime;

            switch (gameOutcome)
            {
                case GameOutcome.None:
                    break;
                case GameOutcome.WinX:
                {
                    _statsModel.PlayerOneWins++;
                }
                    break;
                case GameOutcome.WinO:
                {
                    _statsModel.PlayerTwoWins++;
                }
                    break;
                case GameOutcome.Draw:
                {
                    _statsModel.Draws++;
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameOutcome), gameOutcome, null);
            }
            
            _statsModelRepository.Update(_statsModel);
        }
        
        private void OnTimerTick(float totalElapsedTime)
        {
            _totalMatchTime = totalElapsedTime;
            _gameData.MatchTime = _totalMatchTime;
            _gameModelRepository.Update(_gameData);
        }

        private void ResetCacheData()
        {
            _totalMatchTime = 0f;
            // _gameData.CurrentPlayer = 1;
            // _gameData.Board = new List<int>()
            // {
            //     0, 0, 0,
            //     0, 0, 0,
            //     0, 0, 0
            // };
            // _gameData.MatchResult = GameOutcome.None;
            // _gameData.MatchTime = 0;
            // _gameData.PlayerOneMoves = 0;
            // _gameData.PlayerTwoMoves = 0;
        }
    }
}
