using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
[SerializeField]
public class Details {
    public bool Low;
    public bool high;
    public bool High;
}

public class gpxReader : MonoBehaviour
{
    private const string filename = "Sunday_hike.gpx";
    private List<float> elevation = new List<float>();
    private Vector3[] ScaledEelvation;
    private int LineRenderepoints;
    private LineRenderer lineRenderer;
    [SerializeField]
    private float yScalefactor = 0.01f;
    [SerializeField]
    private float xScalefactor = 0.0001f;
    public bool UltraLow;
    public bool Lowdetail;
    public bool Highdetail;
    public bool Mediumdetail;
    public Details detailLevel;
    private int increase = 1;
    private int elevationCount;
    private const string file = "C:/Users/antho/Downloads/Afternoon_Hike.gpx";
    // Start is called before the first frame update
    void Start()
    {
        XmlSerializer xmlSerialiser = new XmlSerializer(typeof(XmlSerialGpx.Gpx));

        FileStream fs = new FileStream(file, FileMode.Open);
        XmlReader readers = XmlReader.Create(fs);

        XmlSerialGpx.Gpx objgpx = (XmlSerialGpx.Gpx)xmlSerialiser.Deserialize(readers);

        Debug.Log(objgpx.trk.trkseg.trkpt[0].lat);

        // Movetonext();
    }
    void Movetonext()
    {
        XmlTextReader reader = new XmlTextReader(file);
        bool ele = false;
        bool name = false;

       

        while (reader.Read())
        {
            
            switch (reader.NodeType)
            {
                case XmlNodeType.Element: // The node is an element.
                    if (reader.Name == "ele")
                    {
                        ele = true;
                    }

                    if (reader.Name == "name")
                    { name = true; }
                       // while (reader.MoveToNextAttribute())
                       // Debug.Log(" " + reader.Name + "='" + reader.Value + "'");
                       // Debug.Log(reader.Name);
                       // Debug.Log(">>");
                       if(reader.Name == "trkpt")
                    {
                        Debug.Log(reader.Name);
                    }
                    break;

                case XmlNodeType.Text: //Display the text in each element.
                    if (ele)
                    {
                        elevation.Add(float.Parse(reader.Value));
                        ele = false;
                    }
                    else if (name)
                    {
                        Debug.Log(reader.Value);
                        name = false;
                    }
                    //Debug.Log(reader.Value);
                    break;

                case XmlNodeType.EndElement: //Display the end of the element.
                                             //Debug.Log("</" + reader.Name + ">");

                    break;
            }
        }


        float start = 0.000f;
        if (Lowdetail)
        {
            increase = 4;
            LineRenderepoints = (int)Mathf.Round(elevation.Count / 4);
            elevationCount = elevation.Count;
            xScalefactor += xScalefactor * 4;
        }
        else if (Mediumdetail)
        {
            LineRenderepoints = (int)Mathf.Round(elevation.Count / 2);
            elevationCount = elevation.Count;
            increase = 2;
            xScalefactor += xScalefactor * 2;
        }
        else if (Highdetail)
        {
            LineRenderepoints = elevation.Count;
            elevationCount = elevation.Count;
            increase = 1;

        } else if (UltraLow)
        {
            increase = 8;
            LineRenderepoints = (int)Mathf.Round(elevation.Count / 8);
            elevationCount = elevation.Count;
            xScalefactor += xScalefactor * 8;
        }

        ScaledEelvation = new Vector3[elevationCount];
        int ii = 0;
        for (int i = 0; i < elevationCount; i += increase)
        {

            var min = elevation.Min();

            float scaled = (float)((elevation[i] - min) * yScalefactor);

            ScaledEelvation[ii] = new Vector3(start, scaled);
            start += xScalefactor;
            ii++;

        }



        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = LineRenderepoints;
        lineRenderer.SetPositions(ScaledEelvation);

        gameObject.AddComponent<BoxCollider2D>();

        //Simple();

    }
    void Simple()
    {
        XmlTextReader reader = new XmlTextReader(file);

        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element: // The node is an element.
                    Debug.Log("<" + reader.Name + ">");

                    break;

                case XmlNodeType.Text: //Display the text in each element.
                    Debug.Log(reader.Value);
                    break;

                case XmlNodeType.EndElement: //Display the end of the element.
                    Debug.Log("</" + reader.Name + ">");

                    break;
            }
        }
    }

  
    public void ChangeLineWidth(float single)
    {
        
        lineRenderer.startWidth =single * 2;
        lineRenderer.endWidth=single * 2;
    }
}
