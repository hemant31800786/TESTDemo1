using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IFSPRojectTest.Persitance.model
{
    public class JshonDataView
    {
        public Candidate candidate { get; set; }

        public List<Technology> Technology { get; set; }
    }
}