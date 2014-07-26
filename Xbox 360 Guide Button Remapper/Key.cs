using System;

namespace Xbox_360_Guide_Button_Remapper
{
    [Serializable()]
    public class Key
    {
        public string KeyCode { get; set; }
        public int KeyValue { get; set; }

        
        public Key()
        {
            KeyCode = string.Empty;
            KeyValue = 0;
        }

        public Key(int val, string code)
        {
            KeyCode = code;
            KeyValue = val;
        }
    }
}
