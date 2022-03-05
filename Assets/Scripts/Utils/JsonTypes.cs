using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class JsonUtils
    {
        public static CollectableItemJson LoadJson<CollectableItemJson> (string json) {
            return UnityEngine.JsonUtility.FromJson<CollectableItemJson>(json);
        }

        [Serializable]
        public class CollectableItemJson
        {   
            public List<Buff> buff;
            public List<Skill> skill;

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
            
            [Serializable]
            public class Skill{
                public string name;

            }
            

        }

        
    }
}