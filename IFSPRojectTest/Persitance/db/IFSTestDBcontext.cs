using IFSPRojectTest.Persitance.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IFSPRojectTest.Persitance.db
{
    public class IFSTestDBcontext : DbContext
    {

        public IFSTestDBcontext() : base("name=IFS")
        {

        }
        public DbSet<UserManager> UserMaster { get; set; }
        public DbSet<Candidate> CandidateMaster { get; set; }
        public DbSet<Technology> Technology { get; set; }

    }
}