using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(2024122501)]
public class CreateAuthorsTable:Migration
{
    public override void Up()
    {
        Create.Table("Authors")
            .WithColumn("AuthorId").AsInt32().PrimaryKey().Identity()
            .WithColumn("AuthorName").AsString(30).NotNullable()
            .WithColumn("Country").AsString(30).Nullable();
    }

    public override void Down()
    {
        Delete.Table("Authors");
    }
}