using System;

namespace Utils
{

    class JsonUtils
    {
        public static CollectableItemJson[] LoadJson<CollectableItemJson> (string json) {
            return UnityEngine.JsonUtility.FromJson<CollectableItemJson[]>(json);
        }

        [Serializable]
        public class CollectableItemJson
        {   
            public Buff buff;

            [Serializable]
            public class Buff {
                public string name;
                public BuffJson buff;
                
                [Serializable]
                public class BuffJson
                {
                    public string name;
                    public int value;
                }
            }
            
            public Skills skill;

            [Serializable]
            public class Skills{
                public string name;
            }
        }

        
    }
}