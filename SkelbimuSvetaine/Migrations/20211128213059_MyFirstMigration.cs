using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SkelbimuSvetaine.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "category",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(type: "int(11)", nullable: false)
        //                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
        //            name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_category", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "user",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(type: "int(11)", nullable: false)
        //                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
        //            username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
        //            password = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
        //            phone = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
        //            email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
        //            icon = table.Column<byte[]>(type: "blob", nullable: true, defaultValueSql: "'NULL'"),
        //            miestas = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_user", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "product",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(type: "int(11)", nullable: false)
        //                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
        //            name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
        //            description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
        //            price = table.Column<double>(type: "double(10)", nullable: false),
        //            image = table.Column<byte[]>(type: "blob", nullable: false),
        //            User_id = table.Column<int>(type: "int(11)", nullable: false),
        //            Category_id = table.Column<int>(type: "int(11)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_product", x => x.id);
        //            table.ForeignKey(
        //                name: "fk_Product_Category1",
        //                column: x => x.Category_id,
        //                principalTable: "category",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_Product_User",
        //                column: x => x.User_id,
        //                principalTable: "user",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "comment",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(type: "int(11)", nullable: false)
        //                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
        //            Message = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
        //            User_id = table.Column<int>(type: "int(11)", nullable: false),
        //            Product_id = table.Column<int>(type: "int(11)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_comment", x => x.id);
        //            table.ForeignKey(
        //                name: "fk_Comment_Product1",
        //                column: x => x.Product_id,
        //                principalTable: "product",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_Comment_User1",
        //                column: x => x.User_id,
        //                principalTable: "user",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "rating",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(type: "int(11)", nullable: false)
        //                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
        //            value = table.Column<int>(type: "int(11)", nullable: false),
        //            User_id = table.Column<int>(type: "int(11)", nullable: false),
        //            Product_id = table.Column<int>(type: "int(11)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_rating", x => x.id);
        //            table.ForeignKey(
        //                name: "fk_Rating_Product1",
        //                column: x => x.Product_id,
        //                principalTable: "product",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_Rating_User1",
        //                column: x => x.User_id,
        //                principalTable: "user",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "fk_Comment_Product1_idx",
        //        table: "comment",
        //        column: "Product_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_Comment_User1_idx",
        //        table: "comment",
        //        column: "User_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_Product_Category1_idx",
        //        table: "product",
        //        column: "Category_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_Product_User_idx",
        //        table: "product",
        //        column: "User_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_Rating_Product1_idx",
        //        table: "rating",
        //        column: "Product_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_Rating_User1_idx",
        //        table: "rating",
        //        column: "User_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "rating");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
