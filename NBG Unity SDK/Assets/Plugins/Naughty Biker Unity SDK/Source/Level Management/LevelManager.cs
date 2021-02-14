using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Zenject;
using NaughtyBiker.LevelManagement.Interfaces;
using NaughtyBiker.Wrappers.Interfaces;

namespace NaughtyBiker.LevelManagement {
	/**
    * Default implementation of ILevelManager.
    *
    * @author Julian Sangillo
    * @version 1.0
    */
	public class LevelManager : ILevelManager {

        private readonly IDictionary<string, int> levels;
        private readonly ISceneManager sceneManager;
        private readonly ISceneUtility sceneUtility;

        private string[] labels;
        private int activeLevel;
        private string firstLevel;

        /**
        * Default implementation of ILevelManager.ActiveLevelLabel
        *
        * @return The label of the active level
        */
        public string ActiveLevelLabel {
            get {
                return labels[activeLevel];
            }
        }

        /**
        * Default implementation of ILevelManager.ActiveLevel
        *
        * @return The index of the active level
        */
		public int ActiveLevel {
            get {
                return activeLevel;
            }
        }

        /**
        * Default implementation of ILevelManager.FirstLevel
        *
        * @param value The name of the first level
        *
        * @return The name of the first level
        *
        * @throws ArgumentNullException Raised when the name parameter is null
        * @throws ArgumentException Raised when a level by the name provided does not exist. In the event this happens, please verify
        * the scenes added to Build Settings
        */
		public string FirstLevel {
            get {
                return this.firstLevel;
            }
            set {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("FirstLevel", "First level cannot be null!");
                else if(!levels.ContainsKey(value))
                    throw new ArgumentException(String.Format("The level \"{0}\" doesn't exist!", value), "FirstLevel");

                this.firstLevel = value;
            }
        }

		/**
        * Callback from Zenject.IInitializable that is invoked during Unity's Start phase. When called, it pulls the list of
        * scenes from Unity Build Settings and adds them as levels to track, it sets the first level to level 0, it calculates 
        * the labels of the levels, and it sets the active level currently.
        *
        * This function is called automatically by Zenject. Don't call this directly!
        */
		public void Initialize() {
            int level;
            int count = sceneManager.sceneCountInBuildSettings;
            string name;

            labels = new string[count];

            for(int i = 0; i < count; i++) {
                string capsOrNonLetterRegex = @"(?<!\s)(([A-Z]+)|([^A-Za-z\s]+))";
                string underscoreRegex = @"_+";

                level = i;
                name = Path.GetFileNameWithoutExtension(sceneUtility.GetScenePathByBuildIndex(i));

                levels.Add(name, level);

                if(i == 0)
                    firstLevel = name;

                name = Regex.Replace(name, underscoreRegex, " ");
                name = Regex.Replace(name, capsOrNonLetterRegex, m => (" " + m.Value));
                name = name.Trim();

                labels[i] = name;
            }

            activeLevel = sceneManager.GetActiveSceneBuildIndex();
        }

        /**
        * Default implementation of ILevelManager.GetLevel
        * 
        * @param name The name of the level
        *
        * @return The index of the level
        *
        * @throws ArgumentNullException Raised when the name parameter is null
        * @throws ArgumentException Raised when a level by the name provided does not exist. In the event this happens, please verify
        * the scenes added to Build Settings
        */
        public int GetLevel(string name) {
            int level;

            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Name of level cannot be null!");
            else if(!levels.TryGetValue(name, out level))
                throw new ArgumentException(String.Format("The level \"{0}\" cannot be read because it doesn't exist!", name), "name");

			return level;
		}

        /**
        * Default implementation of ILevelManager.GetLevelLabel(int)
        *
        * @param index The build index for the level
        *
        * @return The label of the level
        *
        * @throws ArgumentNullException Raised when the index parameter is a negative value
        * @throws ArgumentException Raised when a level by the index provided does not exist. In the event this happens, please verify
        * the scenes added to Build Settings
        */
		public string GetLevelLabel(int index) {
            if(index < 0)
                throw new ArgumentOutOfRangeException("index", index, "Level index cannot be a negative!");
            else if(index >= labels.Length)
                throw new ArgumentOutOfRangeException("index", index, "The index doesn't exist!");

			return labels[index];
		}

        /**
        * Default implementation of ILevelManager.GetLevelLabel(string)
        *
        * @param name The name of the level
        *
        * @return The label of the level
        *
        * @throws ArgumentNullException Raised when the name parameter is null
        * @throws ArgumentException Raised when a level by the name provided does not exist. In the event this happens, please verify
        * the scenes added to Build Settings
        */
		public string GetLevelLabel(string name) {
            int index;

            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Name of level cannot be null!");
            else if(!levels.TryGetValue(name, out index))
                throw new ArgumentException(String.Format("The level \"{0}\" cannot be read because it doesn't exist!", name), "name");

			return labels[index];
		}

        /**
        * Default implementation of ILevelManager.LoadLevel(string name)
        * 
        * @param name The name of the level to load. This is the actual name of the scene as shown in Build Settings, not the display
        * name!
        * 
        * @throws ArgumentNullException Raised when the name parameter is null
        * @throws ArgumentException Raised when a level by the name provided does not exist. In the event this happens, please verify
        * the scenes added to Build Settings
        */
        public void LoadLevel(string name) {
            int newLevel = GetLevel(name);

            sceneManager.LoadScene(newLevel);
            activeLevel = newLevel;
        }

        /**
        * Loads next level according to the scene's build index and the order the scenes are listed in Build Settings. This allows
        * you to let LevelManager determine the next level and take you there automatically, in the case that levels are progressed
        * sequentially in your game, without having to keep track of all level names. If there is no next level, this will take you
        * back to the first scene listed in Build Settings.
        *
        * Default implementation of ILevelManager.LoadNextLevel()
        */
        public void LoadNextLevel() {
            activeLevel++;

            if(activeLevel < sceneManager.sceneCountInBuildSettings)
                sceneManager.LoadScene(activeLevel);
            else {
                sceneManager.LoadScene(0);
                activeLevel = 0;
            }
        }

        /**
        * Loads previous level according to the scene's build index and the order the scenes are listed in Build Settings. This allows
        * you to let LevelManager determine the previous level and take you there automatically, in the case that levels are progressed
        * sequentially in your game, without having to keep track of all the level names. If there is no previous level, this will raise
        * an InvalidOperationException.
        * 
        * Default implementation of ILevelManager.LoadPreviousLevel()
        *
        * @throws InvalidOperationException Raised when you are already on level index 0 and there is no previous level to load
        */
        public void LoadPreviousLevel() {
			activeLevel--;

            if(activeLevel >= 0)
                sceneManager.LoadScene(activeLevel);
            else {
                activeLevel = 0;
                throw new InvalidOperationException("You are already on level index 0. No previous level to load!");
            }
		}

        /**
        * Loads the first level, which is index 0 by default, but can be changed by setting the FirstLevel property. This is useful if
        * you want to bring a player back to the first level repeatedly during the game, but you don't want to repeatedly give the level
        * name. It is also useful to be able to choose which level is the "first level" because index 0 is typically the main menu in a
        * lot of games, which may not be where you actually want to bring the player.
        *
        * Default implementation of ILevelManager.LoadFirstLevel()
        */
		public void LoadFirstLevel() {
            int index = levels[firstLevel];

			sceneManager.LoadScene(index);
            activeLevel = index;
		}

        /**
        * Default implementation of ILevelManager.ReloadLevel()
        */
        public void ReloadLevel() {

            sceneManager.LoadScene(activeLevel);

        }

		[Inject]
        private LevelManager(IDictionary<string, int> levels, ISceneManager sceneManager, ISceneUtility sceneUtility) {

            this.levels = levels;
            this.sceneManager = sceneManager;
            this.sceneUtility = sceneUtility;

        }
        
    }
}