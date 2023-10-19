using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ButterCMS.Tests.Models
{
    public class TeamMembersHeadline
    {
        [JsonProperty("team_members")]
        public TeamMembers[] TeamMembers { get; set; }
        [JsonProperty("homepage_headline")]
        public string HomepageHeadline { get; set; }
    }

    public class TeamMembers
    {
        public string Bio { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
    }

}
