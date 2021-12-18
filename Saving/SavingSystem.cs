using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingSystem
{    
    const string m_SaveFile = "save";                     
    public void Save(LevelScore score)
    {            
        string path = GetPathFromSaveFile(m_SaveFile);
        using (FileStream stream = File.Open(path, FileMode.Append))
        {                
            Debug.Log("SAVING");
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, score.level);
            formatter.Serialize(stream, score.time);
            formatter.Serialize(stream, score.moves);
            formatter.Serialize(stream, score.pushes);
        }
    }
    public LevelScore Load(string level)
    {
        string path = GetPathFromSaveFile(m_SaveFile);
        LevelScore score = new LevelScore("TBD", -1f, -1, -1);        
        if(!File.Exists(path)) return score;

        using (FileStream stream = File.Open(path, FileMode.Open))
        {            
            while(stream.Position != stream.Length)
            {                
                BinaryFormatter formatter = new BinaryFormatter();
                string file_level = (string)formatter.Deserialize(stream);
                float file_time = (float)formatter.Deserialize(stream);
                int file_moves = (int)formatter.Deserialize(stream);
                int file_pushes = (int)formatter.Deserialize(stream);  
                if(level == file_level && FileScoreIsBetter(score, file_time, file_moves, file_pushes))
                {
                    Debug.Log(file_level);
                    Debug.Log(file_time);
                    Debug.Log(file_moves);
                    Debug.Log(file_pushes);
                    score.level = file_level;              
                    score.time = file_time;
                    score.moves = file_moves;
                    score.pushes = file_pushes;
                }
            }
        }                                
        return score;
    }
    private string GetPathFromSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }        
    private bool FileScoreIsBetter(LevelScore score, float time, int moves, int pushes)
    {
        if(score.level == "TBD") return true;
        return score.time > time;
        // float current_points = score.time * 0.5f - (float)score.moves * 0.25f - (float)score.pushes * 0.25f;
        // float new_points = time * 0.5f - (float)moves * 0.25f - (float)pushes * 0.25f;
        
        // return current_points > new_points;
    }
}
