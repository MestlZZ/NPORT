using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NPORT.Models
{
    [Serializable]
    [XmlRoot("News"), XmlType("News")]
    public class NewsModels
    {
        public int NewsId { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string Autor { get; set; }
    }
}
