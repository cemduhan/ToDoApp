namespace ToDoApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firtMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoLists",
                c => new
                    {
                        ToDoListId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        ToDoListUserId = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ToDoListId);
            
            CreateTable(
                "dbo.ToDoListItems",
                c => new
                    {
                        ToDoListItemId = c.Int(nullable: false, identity: true),
                        ToDoListId = c.Int(nullable: false),
                        name = c.String(),
                        explanation = c.String(),
                        status = c.String(),
                        deadline = c.DateTime(nullable: false),
                        created_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ToDoListItemId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        hashedpassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.ToDoListItems");
            DropTable("dbo.ToDoLists");
        }
    }
}
