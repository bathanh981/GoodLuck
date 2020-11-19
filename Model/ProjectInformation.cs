using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model
{
   public class ProjectInformation
    {
        private string projectName;
        private string address;
        private string engineerName;

        public string ProjectName { get => projectName; set => projectName = value; }
        public string Address { get => address; set => address = value; }
        public string EngineerName { get => engineerName; set => engineerName = value; }

        public ProjectInformation()
        {
        }

        public ProjectInformation(string projectName, string address, string engineerName)
        {
            ProjectName = projectName;
            Address = address;
            EngineerName = engineerName;
        }
    }
}
