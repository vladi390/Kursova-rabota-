using System;
using System.Collections.Generic;
using System.Text;

namespace GalaxyCatalog.Modals
{
    public class Moon: ICountList
    {
        
        private string moonName;
        static int instances = 0;

        
        public Moon(string moonName)
        {
            this.moonName = moonName;

            instances++;
        }

        
        public string MoonName {
            get { return this.moonName;  }
        }

        
        public static int GetActiveInstances()
        {
            return instances;
        }
        int ICountList.GetActiveInstances()
        {
            return Moon.GetActiveInstances();
        }

        public string GetInnerList(string view)
        {
            return "";
        }


        public override string ToString()
        {
            return this.moonName;
        }

    }
}
