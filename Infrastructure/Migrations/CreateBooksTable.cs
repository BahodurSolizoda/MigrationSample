using FluentMigrator;

namespace Infrastructure.Migrations;


[Migration(2024122502)]
public class CreateBooksTable:Migration
{
    public override void Up()
    {
        Create.Table("Books")
            .WithColumn("BookId").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("AuthorId").AsInt32()
            .WithColumn("PublishedYear").AsInt32().NotNullable()
            .WithColumn("Genre").AsString().NotNullable()
            .WithColumn("IsAvailable").AsBoolean().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Books");
    }
}