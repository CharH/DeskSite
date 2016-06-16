using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;

namespace DeskTopper
{
    public enum SurfaceMaterial
    {
        Oak,
        Laminate,
        Pine,
        Marble,
        Walnut,
        Metal
    }

    class Desk
    {
        public string orderName { get; set; }
        public int deskWidth { get; set; }
        public int deskLength { get; set; }
        public int noOfDrawers { get; set; }
        public int rushOrder { get; set; }
        public SurfaceMaterial deskTopType { get; set; }
        public int priceEstimate { get; set; }
        
    }
}
 
        

