using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class GameDataController : UnitySingleton<GameDataController>
{
    public Highscore CurrentHighScore;

    private XmlSerializer xmlSerializer = new XmlSerializer(typeof(Highscore));
    private FileStream fileStream;
    private  string dataPath = "";
    private const string DATA_NAME = "save.xml";
    private string savePathAndData;

    void Awake()
    {
        dataPath = Application.dataPath;
        savePathAndData = Path.Combine(dataPath, DATA_NAME);
    }

    public void LoadHighScore()
    {
        //var serializer = new XmlSerializer(typeof(Highscore));
        //var stream = new FileStream(dataPath, FileMode.Open);
        //var container = serializer.Deserialize(stream) as Highscore;
        //stream.Close();

        //using (fileStream = new FileStream(savePathAndData, FileMode.Open))
        //{
        //    CurrentHighScore = (Highscore)xmlSerializer.Deserialize(fileStream);
        //    print(CurrentHighScore.Seconds);
        //}
    }

    public void SaveHighScore(Highscore highscore)
    {
        //using (fileStream = new FileStream(savePathAndData, FileMode.Create))
        //{
        //    xmlSerializer.Serialize(fileStream, highscore);
        //}
    }

}
