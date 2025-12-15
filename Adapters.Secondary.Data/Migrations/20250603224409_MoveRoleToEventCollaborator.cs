using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapters.Secondary.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveRoleToEventCollaborator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventCollaborators_Collaborators_CollaboratorsId",
                table: "EventCollaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_EventCollaborators_Events_EventsId",
                table: "EventCollaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCollaborators",
                table: "EventCollaborators");

            migrationBuilder.DropIndex(
                name: "IX_EventCollaborators_EventsId",
                table: "EventCollaborators");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "EventCollaborators",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "CollaboratorsId",
                table: "EventCollaborators",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Collaborators",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EventCollaborators",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "EventCollaborators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EventCollaborators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EventCollaborators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCollaborators",
                table: "EventCollaborators",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventCollaborators_CollaboratorId",
                table: "EventCollaborators",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCollaborators_EventId_CollaboratorId",
                table: "EventCollaborators",
                columns: new[] { "EventId", "CollaboratorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventCollaborators_Collaborators_CollaboratorId",
                table: "EventCollaborators",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventCollaborators_Events_EventId",
                table: "EventCollaborators",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventCollaborators_Collaborators_CollaboratorId",
                table: "EventCollaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_EventCollaborators_Events_EventId",
                table: "EventCollaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventCollaborators",
                table: "EventCollaborators");

            migrationBuilder.DropIndex(
                name: "IX_EventCollaborators_CollaboratorId",
                table: "EventCollaborators");

            migrationBuilder.DropIndex(
                name: "IX_EventCollaborators_EventId_CollaboratorId",
                table: "EventCollaborators");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventCollaborators");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "EventCollaborators");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EventCollaborators");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EventCollaborators");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "EventCollaborators",
                newName: "EventsId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventCollaborators",
                newName: "CollaboratorsId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Collaborators",
                newName: "Role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventCollaborators",
                table: "EventCollaborators",
                columns: new[] { "CollaboratorsId", "EventsId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventCollaborators_EventsId",
                table: "EventCollaborators",
                column: "EventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventCollaborators_Collaborators_CollaboratorsId",
                table: "EventCollaborators",
                column: "CollaboratorsId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventCollaborators_Events_EventsId",
                table: "EventCollaborators",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
