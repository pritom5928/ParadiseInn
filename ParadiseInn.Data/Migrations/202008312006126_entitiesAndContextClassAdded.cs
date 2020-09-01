﻿namespace ParadiseInn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitiesAndContextClassAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NoOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccomodationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccomodationTypes", t => t.AccomodationTypeId, cascadeDelete: true)
                .Index(t => t.AccomodationTypeId);
            
            CreateTable(
                "dbo.AccomodationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccomodationName = c.String(),
                        Description = c.String(),
                        AccomodationPackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageId, cascadeDelete: true)
                .Index(t => t.AccomodationPackageId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        AccomodationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .Index(t => t.AccomodationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.Accomodations", "AccomodationPackageId", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPackages", "AccomodationTypeId", "dbo.AccomodationTypes");
            DropIndex("dbo.Bookings", new[] { "AccomodationId" });
            DropIndex("dbo.Accomodations", new[] { "AccomodationPackageId" });
            DropIndex("dbo.AccomodationPackages", new[] { "AccomodationTypeId" });
            DropTable("dbo.Bookings");
            DropTable("dbo.Accomodations");
            DropTable("dbo.AccomodationTypes");
            DropTable("dbo.AccomodationPackages");
        }
    }
}
