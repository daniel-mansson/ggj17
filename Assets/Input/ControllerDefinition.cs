using UnityEngine;
using System.Collections;

public class ControllerDefinition
{
    public class Joystick
    {
        public int HorizontalAxis ;
        public int VerticalAxis ;
        public double Deadzone;
    }

    public class Trigger
    {
        public int Axis ;
        public double Deadzone ;
    }

    public class ControllerData
    {
        public string Name ;
        public string ControllerNameRegex ;
        public Joystick[] Joysticks ;
        public Trigger[] Triggers ;
        public int[] Buttons ;
    }

    ControllerData m_data;

    public ControllerDefinition(string jsonData)
    {
        m_data = LitJson.JsonMapper.ToObject<ControllerData>(jsonData);
    }

    public ControllerDefinition(ControllerData data)
    {
        m_data = data;
    }

    public ControllerData Data
    {
        get { return m_data; }
    }
}
