using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID = "47471bba-7025-48a9-a9b1-b22f7e82ad08";
        const string ADMIN_ROLE_GUID = "c7c0f0b7-5c7a-4821-b7c5-34508e3bdb84";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher=new PasswordHasher<ApplicationUser>();

            var passwordhash=hasher.HashPassword(null, "Admin123!");

            StringBuilder sb=new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers (Id, " +
                                                    "UserName, " +
                                                    "NormalizedUserName, " +
                                                    "Email, " +
                                                    "NormalizedEmail, " +
                                                    "EmailConfirmed, " +
                                                    "PasswordHash, " +
                                                    "SecurityStamp, " +
                                                    "PhoneNumber, " +
                                                    "PhoneNumberConfirmed, " +
                                                    "TwoFactorEnabled, " +
                                                    "LockoutEnabled, " +
                                                    "AccessFailedCount, " +
                                                    "Address, " +
                                                    "City, " +
                                                    "Country, " +
                                                    "FirstName, " +
                                                    "LastName, " +
                                                    "PostalCode) ");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}',");
            sb.AppendLine($"'admin@admin.com',");
            sb.AppendLine($"'ADMIN@ADMIN.COM',");
            sb.AppendLine($"'admin@admin.com',");
            sb.AppendLine($"'ADMIN@ADMIN.COM',");
            sb.AppendLine($"0,");
            sb.AppendLine($"'{passwordhash}',");
            sb.AppendLine($"'Admin',");
            sb.AppendLine($"'+385981234567',");
            sb.AppendLine($"1,");
            sb.AppendLine($"0,");
            sb.AppendLine($"0,");
            sb.AppendLine($"0,");
            sb.AppendLine($"'Trg Bana Jelačića 1',");
            sb.AppendLine($"'Zagreb',");
            sb.AppendLine($"'Croatia',");
            sb.AppendLine($"'Josip',");
            sb.AppendLine($"'Jelačić',");
            sb.AppendLine($"'10000'");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}', 'Admin', 'ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}', '{ADMIN_ROLE_GUID}')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId='{ADMIN_USER_GUID}' AND RoleId='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id='{ADMIN_USER_GUID}'");
        }
    }
}
