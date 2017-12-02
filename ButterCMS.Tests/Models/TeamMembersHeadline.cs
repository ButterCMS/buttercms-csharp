using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButterCMS.Tests.Models
{
    public class TeamMembersHeadline
    {
        public Team_Members[] team_members { get; set; }
        public string homepage_headline { get; set; }
    }

    public class Team_Members
    {
        public string bio { get; set; }
        public string picture { get; set; }
        public string name { get; set; }
    }

}
