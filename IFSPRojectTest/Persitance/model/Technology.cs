using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IFSPRojectTest.Persitance.model
{
    public class Technology
    {
        [Required]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechnologyId { get; set; }

        public Candidate Candidate { get; set; }
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("experianceYears")]
        public int experianceYears { get; set; }

    }
}