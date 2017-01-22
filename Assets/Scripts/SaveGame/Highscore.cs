using System.Xml;
using System.Xml.Serialization;

public class Highscore
{
    [XmlAttribute("name")]
    public int Seconds;

    public Highscore()
    {

    }
}
