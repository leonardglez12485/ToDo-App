﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_persons_PersonId",
                table: "tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "tasks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_persons_PersonId",
                table: "tasks",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_persons_PersonId",
                table: "tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_persons_PersonId",
                table: "tasks",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
