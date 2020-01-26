using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IFSPRojectTest.Persitance.model
{
    public class Candidate
    {
        [Required]
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateId { get; set; }

        [JsonProperty("_id")]
        public string JID { get; set; }

        [JsonProperty("index")]
        public int index { get; set; }

        [JsonProperty("guid")]
        public string guid { get; set; }

        [JsonProperty("isActive")]
        public string isActive { get; set; }

        [JsonProperty("age")]
        public int age { get; set; }

        [JsonProperty("showSize")]
        public int showSize { get; set; }

        [JsonProperty("eyeColor")]
        public string eyeColor { get; set; }

        [JsonProperty("name")]
        public Name name { get; set; }

        [JsonProperty("currentCompany")]
        public string currentCompany { get; set; }

        [JsonProperty("picture")]
        public string picture { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("about")]
        public string about { get; set; }

        [JsonProperty("fullResume")]
        public string fullResume { get; set; }

        [JsonProperty("lastKnownLocation")]
        public LastKnownLocation lastKnownLocation { get; set; }

        [JsonProperty("technologies")]
        public IEnumerable<Technology> Technology { get; set; }

        [JsonProperty("languages")]
        public IEnumerable<string> languages { get; set; }

        [JsonProperty("favoriteFruit")]
        public string favoriteFruit { get; set; }

        public bool? isAccepted { get; set; }

        public bool? isRecruited { get; set; }

    }

    public class Name
    {
        [JsonProperty("first")]
        public string Firstname { get; set; }

        [JsonProperty("last")]
        public string Lastname { get; set; }
    }

    public class LastKnownLocation
    {
        [JsonProperty("latitude")]
        public string latitude { get; set; }

        [JsonProperty("longitude")]
        public string longitude { get; set; }
    }
}