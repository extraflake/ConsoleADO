namespace Bootcamp.CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelTransaction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionsItems", "Suppliers_Id", "dbo.Suppliers");
            DropIndex("dbo.TransactionsItems", new[] { "Suppliers_Id" });
            AddColumn("dbo.TransactionsItems", "Transaction_Id", c => c.Int());
            AlterColumn("dbo.TransactionsItems", "Quantity", c => c.Int());
            CreateIndex("dbo.TransactionsItems", "Transaction_Id");
            AddForeignKey("dbo.TransactionsItems", "Transaction_Id", "dbo.Transactions", "Id");
            DropColumn("dbo.TransactionsItems", "Suppliers_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionsItems", "Suppliers_Id", c => c.Int());
            DropForeignKey("dbo.TransactionsItems", "Transaction_Id", "dbo.Transactions");
            DropIndex("dbo.TransactionsItems", new[] { "Transaction_Id" });
            AlterColumn("dbo.TransactionsItems", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionsItems", "Transaction_Id");
            CreateIndex("dbo.TransactionsItems", "Suppliers_Id");
            AddForeignKey("dbo.TransactionsItems", "Suppliers_Id", "dbo.Suppliers", "Id");
        }
    }
}
