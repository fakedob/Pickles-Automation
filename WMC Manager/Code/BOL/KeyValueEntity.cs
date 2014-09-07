using System;
namespace PicklesAutomation
{
    [Serializable]
    public class KeyValueEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public string LongDesc { get; set; }
    }
}
