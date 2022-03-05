using System;

namespace Utils
{

    class JsonUtils
    {
        public static T[] LoadJsonArray<T>(string json) {
            return UnityEngine.JsonUtility.FromJson<WrapperJson<T>>("{\"items\":" + json + "}").items;
        }

        [Serializable]
        public class WrapperJson<T>
        {
            public T[] items;
        }

        [Serializable]
        public class CollectableItemsListJson
        {
            public CollectableItemJson[] array;

            [Serializable]
            public class CollectableItemJson
            {
                public string name;
                public BuffJson buff;
            }

            [Serializable]
            public class BuffJson
            {
                public string name;
                public string value;
            }
        }
    }
}