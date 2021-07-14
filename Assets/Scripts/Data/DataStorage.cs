//**************************************************
// DataStorage.cs
//
// Code Soldiers 2021
//
// Author: Rafał Kania
// Creation Date: 23 June 2021
//**************************************************

//#define DATA_DEBUG

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeSoldiers
{
	public class DataStorage : MonoBehaviour
	{
        private HighScoreData _highScoreData;
        private GameData _gameData;

        private MainManager _mainManager;

        public void InitializeData(MainManager mainManager)
        {
            _mainManager = mainManager;

            string destination = Application.persistentDataPath + Keys.FileSave.FILE_DESTINATION;

            if (File.Exists(destination))
            {
                LoadGameData();
            }
            else
            {
                SaveGameData();
            }
        }

        public void SaveGameData()
        {
            string destination = Application.persistentDataPath + Keys.FileSave.FILE_DESTINATION;
            FileStream file;

            if (File.Exists(destination))
            {
                file = File.OpenWrite(destination);
            }
            else
            {
                file = File.Create(destination);
            }

            GameData data = new GameData(_mainManager?._HighScoreDataToSave);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();

#if DATA_DEBUG
            Debug.Log("Game Saved.");
#endif
        }

        public void LoadGameData()
        {
            string destination = Application.persistentDataPath + Keys.FileSave.FILE_DESTINATION;
            FileStream file;

            if (File.Exists(destination))
                file = File.OpenRead(destination);
            else
            {
#if DATA_DEBUG
                Debug.LogError("File not found.");
#endif
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            _mainManager._HighScoreDataToSave = data._HighScoresData;

            _gameData = data;

#if DATA_DEBUG
            Debug.Log("Data loaded.");
#endif

        }

#if UNITY_EDITOR
        /// <summary>
        /// Delete saved data.
        /// </summary>
        [MenuItem("Utils/Data/Delete Saved Data")]
        public static void DeleteSavedData()
        {
            string destination = Application.persistentDataPath + Keys.FileSave.FILE_DESTINATION;
#if DATA_DEBUG
            Debug.Log("Removing Saved Data.");
#endif
            if (File.Exists(destination))
            {
                File.Delete(destination);
#if DATA_DEBUG
                Debug.Log("File Removed.");
#endif
            }
            else
            {
#if DATA_DEBUG
                Debug.LogError("File not found.");
#endif
            }
        }
#endif
    }
}
