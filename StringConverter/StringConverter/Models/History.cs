using System;

namespace StringConverter.Models
{
    public class History
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SrcText { get; set; }
        public string DesText { get; set; }
        public int Method { get; set; }
    }
}
