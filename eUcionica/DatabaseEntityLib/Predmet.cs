using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Predmet
    {
        public int ID { get; set; }
        public string Ime { get; set; }

        public ICollection<Pitanje> PredmetnaPitanja { get; set; } =  new List<Pitanje>();
        public ICollection<Oblast>  OblastPredmeta {  get; set; } = new List<Oblast>();
    }
}
