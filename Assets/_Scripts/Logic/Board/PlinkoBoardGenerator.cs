using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LOGIC.BOARD
{
    public class PlinkoBoardGenerator : MonoBehaviour
    {
        #region CONSTANTS

        private const int MIN_PEGS_COUNT_IN_ROW = 2;
        private const int MAX_PEGS_COUNT_IN_ROW = 7;

        private const int MIN_INTERVAL_BETWEEN_PEGS = 3;
        private const int MAX_INTERVAL_BETWEEN_PEGS = 5;

        #endregion
        [SerializeField] private GameObject _pegPrefab;
        [SerializeField] private int _initialPegs = 3;
        [SerializeField] private int _rows = 5;
        [SerializeField] private int _columns = 9;
        [SerializeField] private float _xOffset = 1.0f;
        [SerializeField] private float _yOffset = 1.0f;
        [SerializeField] private float _dropHeight = 10.0f;
        [SerializeField] private bool _randomizeBoard = false;
        [SerializeField] private Transform _board;

        public GameObject PegPrefab => _pegPrefab;
        public int InitialPegs => _initialPegs;
        public int Rows => _rows;
        public int Columns => _columns;
        public float XOffset => _xOffset;
        public float YOffset => _yOffset;
        public float DropHeight => _dropHeight;

        public void GenerateRandomPlinkoBoard()
        {
            int previousInterval = Int32.MinValue;

            for (int row = 0; row < _rows; row++)
            {
                int randomIntervalBetweenPegs;
                var randomPegsCount = GetRandomPegsCount();
                
                do
                {
                    randomIntervalBetweenPegs = GetRandomIntervalBetweenPegs();
                } while (previousInterval == randomIntervalBetweenPegs);

                previousInterval = randomIntervalBetweenPegs;
                var targetYPosition = row * -_yOffset;
                var startX = -((randomPegsCount - 1) * randomIntervalBetweenPegs) / 2;
                
                for (int peg = 0; peg < randomPegsCount; peg++)
                {
                    int newXPosition = startX + peg * randomIntervalBetweenPegs;
                    Vector3 pegPosition = new Vector3(newXPosition, targetYPosition, 0);
                    Instantiate(_pegPrefab, pegPosition, _pegPrefab.transform.rotation, _board);
                }
            }
        }

        public void DestroyBoard()
        {
            Peg[] gameObjects = _board.GetComponentsInChildren<Peg>();
            foreach (var element in gameObjects)
            {
                Destroy(element.gameObject);
            }
        }

        private void GeneratePlinkoBoard()
        {
            for (int row = 0; row < _rows; row++)
            {
                int pegsInRow = Mathf.Min(_initialPegs + row, _columns);
                float startX = -((pegsInRow - 1) * _xOffset) / 2;

                for (int peg = 0; peg < pegsInRow; peg++)
                {
                    float xPos = startX + peg * _xOffset;
                    Vector3 pegPosition = new Vector3(xPos, row * -_yOffset, 0);
                    Instantiate(_pegPrefab, pegPosition, _pegPrefab.transform.rotation, transform);
                }
            }
        }

        private int GetRandomPegsCount() 
            => Random.Range(MIN_PEGS_COUNT_IN_ROW, MAX_PEGS_COUNT_IN_ROW + 1);

        private int GetRandomIntervalBetweenPegs() 
            => Random.Range(MIN_INTERVAL_BETWEEN_PEGS, MAX_INTERVAL_BETWEEN_PEGS + 1);
    }
}
