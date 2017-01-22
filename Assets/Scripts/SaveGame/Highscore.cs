using System.Xml;
using System.Xml.Serialization;

public class Highscore
{
    [XmlElement("name")]
    public int Seconds;

    public Highscore()
    {

    }
}
