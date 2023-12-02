namespace ForcedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackagePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccomodationPackages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationTypeID = c.Int(nullable: false),
                        Name = c.String(),
                        Descriprion = c.String(),
                        NoOfPeople = c.String(),
                        FeePerDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationTypes", t => t.AccomodationTypeID, cascadeDelete: true)
                .Index(t => t.AccomodationTypeID);
            
            CreateTable(
                "dbo.AccomodationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccomodationPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationID, cascadeDelete: true)
                .Index(t => t.AccomodationID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageID = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        PictureURL = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationID, cascadeDelete: true)
                .Index(t => t.AccomodationID);
            
            CreateTable(
                "dbo.tblCategories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        Sorting = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.tblOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblOrders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.tblProducts", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.OrderId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tblOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tblProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.tblPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Slug = c.String(),
                        Body = c.String(),
                        Sorting = c.Int(nullable: false),
                        HasSidebar = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.tblSidebar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VenueModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                        Details = c.String(),
                        HasBath = c.Boolean(nullable: false),
                        AccomodationTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationTypes", t => t.AccomodationTypeID, cascadeDelete: true)
                .Index(t => t.AccomodationTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenueModels", "AccomodationTypeID", "dbo.AccomodationTypes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tblOrderDetails", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.tblOrderDetails", "ProductId", "dbo.tblProducts");
            DropForeignKey("dbo.tblProducts", "CategoryId", "dbo.tblCategories");
            DropForeignKey("dbo.tblOrderDetails", "OrderId", "dbo.tblOrders");
            DropForeignKey("dbo.tblOrders", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "AccomodationID", "dbo.Accomodations");
            DropForeignKey("dbo.AccomodationPictures", "AccomodationID", "dbo.Accomodations");
            DropForeignKey("dbo.Accomodations", "AccomodationPackageID", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccomodationPackages", "AccomodationTypeID", "dbo.AccomodationTypes");
            DropForeignKey("dbo.AccomodationPackagePictures", "AccomodationPackageID", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPackagePictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.VenueModels", new[] { "AccomodationTypeID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tblProducts", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.tblOrders", new[] { "ApplicationUserId" });
            DropIndex("dbo.tblOrderDetails", new[] { "ProductId" });
            DropIndex("dbo.tblOrderDetails", new[] { "ApplicationUserId" });
            DropIndex("dbo.tblOrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Bookings", new[] { "AccomodationID" });
            DropIndex("dbo.Accomodations", new[] { "AccomodationPackageID" });
            DropIndex("dbo.AccomodationPictures", new[] { "PictureID" });
            DropIndex("dbo.AccomodationPictures", new[] { "AccomodationID" });
            DropIndex("dbo.AccomodationPackages", new[] { "AccomodationTypeID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "PictureID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "AccomodationPackageID" });
            DropTable("dbo.VenueModels");
            DropTable("dbo.tblSidebar");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tblPages");
            DropTable("dbo.tblProducts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tblOrders");
            DropTable("dbo.tblOrderDetails");
            DropTable("dbo.tblCategories");
            DropTable("dbo.Bookings");
            DropTable("dbo.Accomodations");
            DropTable("dbo.AccomodationPictures");
            DropTable("dbo.AccomodationTypes");
            DropTable("dbo.AccomodationPackages");
            DropTable("dbo.Pictures");
            DropTable("dbo.AccomodationPackagePictures");
        }
    }
}
