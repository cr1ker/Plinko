using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LOGIC.GameStateMachine
{
    public class GameStateMachine : IInitializable, IStartable, ITickable
    {

        private IDictionary<Type, IGameState> _gameStates;
        private IGameState _currentState;
        public IGameState CurrentState => _currentState;
        [Inject] private IObjectResolver _objectResolver;
        
        #region MONO

        public void Initialize()
        {
            InitializeStates();
        }

        public void Start()
        {
            SetLoadingState();
        }

        public void Tick()
        {
            _currentState.Update();
        }

        #endregion

        public void SetPlayingState()
        {
            var playingState = GetState<GamePlayingState>();
            SetState(playingState);
        }

        public void SetLoadingState()
        {
            var loadingState = GetState<GameLoadingState>();
            SetState(loadingState);
        }

        public void SetCompletedLevelState()
        {
            var completedLevelState = GetState<GameCompletedLevelState>();
            SetState(completedLevelState);
        }
        
        private void InitializeStates()
        {
            _gameStates = new Dictionary<Type, IGameState>();
            
            _gameStates[typeof(GamePlayingState)] = new GamePlayingState(this, _objectResolver);
            _gameStates[typeof(GameLoadingState)] = new GameLoadingState(this, _objectResolver);
            _gameStates[typeof(GameCompletedLevelState)] = new GameCompletedLevelState(this, _objectResolver);
        }
        
        private void SetState(IGameState newState)
        {
            _currentState?.Exit();

            _currentState = newState;
            _currentState.Enter();
        }

        private IGameState GetState<T>() where T : IGameState
        {
            var typeOfState = typeof(T);
            return _gameStates[typeOfState];
        }
    }
}
