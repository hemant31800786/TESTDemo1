namespace IFSPRojectTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        JID = c.String(),
                        index = c.Int(nullable: false),
                        guid = c.String(),
                        isActive = c.String(),
                        age = c.Int(nullable: false),
                        showSize = c.Int(nullable: false),
                        eyeColor = c.String(),
                        name_Firstname = c.String(),
                        name_Lastname = c.String(),
                        currentCompany = c.String(),
                        picture = c.String(),
                        email = c.String(),
                        phone = c.String(),
                        address = c.String(),
                        about = c.String(),
                        fullResume = c.String(),
                        lastKnownLocation_latitude = c.String(),
                        lastKnownLocation_longitude = c.String(),
                        favoriteFruit = c.String(),
                        isAccepted = c.Boolean(),
                        isRecruited = c.Boolean(),
                    })
                .PrimaryKey(t => t.CandidateId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        name = c.String(),
                        experianceYears = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TechnologyId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.UserManagers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Technologies", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Technologies", new[] { "CandidateId" });
            DropTable("dbo.UserManagers");
            DropTable("dbo.Technologies");
            DropTable("dbo.Candidates");
        }
    }
}
