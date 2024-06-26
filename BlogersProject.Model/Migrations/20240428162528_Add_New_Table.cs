﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogersProject.Model.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnapprovedBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogFirst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Blogger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnapprovedBlogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnapprovedBlogs");
        }
    }
}
