using System;

namespace LOGIC.Level
{
    public class LevelService
    {
        #region CONSTANTS

        private const string CURRENT_LEVEL = nameof(CURRENT_LEVEL);

        #endregion
        #region EVENTS

        public event Action EventOnLevelComplete;

        #endregion
        
        public int CurrentLevel  
        { 
            get
            {
                SaveManager.GetData(CURRENT_LEVEL, out int currentLevel);
                if (currentLevel == 0)
                {
                    currentLevel = 1;
                    SaveManager.SaveData(CURRENT_LEVEL, currentLevel);
                }
            
                return currentLevel;   
            }
        }

        
        
        public void CompleteLevel()
        {
            IncrementLevel();
            EventOnLevelComplete?.Invoke();
        }
        
        private void IncrementLevel()
        {
            int newLevel = CurrentLevel + 1;
            SaveManager.SaveData(CURRENT_LEVEL, newLevel);
        }
    }
}

