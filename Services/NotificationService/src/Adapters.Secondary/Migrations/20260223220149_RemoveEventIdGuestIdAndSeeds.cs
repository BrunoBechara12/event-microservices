using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adapters.Secondary.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEventIdGuestIdAndSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_EventId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_GuestId",
                table: "Notifications");

            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Notifications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Notifications",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MessageTemplates",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "Template", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Event Invitation", "Olá {guestName}! 🎉\n\nVocê foi convidado para o evento *{eventName}*.\n\n📅 Data: {eventDate}\n\nEsperamos você lá!", 0, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Event Reminder", "Lembrete! ⏰\n\nO evento *{eventName}* está chegando!\n\n📅 Data: {eventDate}\n\nNão se esqueça!", 1, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Invite Confirmation", "Obrigado, {guestName}! ✅\n\nSua presença no evento *{eventName}* foi confirmada.\n\nAté lá!", 4, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Event Cancellation", "Atenção, {guestName}! ❌\n\nInfelizmente o evento *{eventName}* foi cancelado.\n\nPedimos desculpas pelo inconveniente.", 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EventId",
                table: "Notifications",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_GuestId",
                table: "Notifications",
                column: "GuestId");
        }
    }
}
