using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    birthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    authorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    birthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    nickname = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.authorId);
                });

            migrationBuilder.CreateTable(
                name: "BookType",
                columns: table => new
                {
                    typeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookType", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    genreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.genreId);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    positionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    salary = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.positionId);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    publisherId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.publisherId);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    readerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    birthDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.readerId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    roomNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    seatCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.roomNumber);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.tagId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRol~",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUse~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUse~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRole~",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUser~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUse~",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    dateOfEmployment = table.Column<DateTime>(type: "TEXT", nullable: false),
                    positionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Position_position~",
                        column: x => x.positionId,
                        principalTable: "Position",
                        principalColumn: "positionId");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ISBN = table.Column<long>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false),
                    availableCopys = table.Column<int>(type: "INTEGER", nullable: false),
                    ratingAVG = table.Column<double>(type: "REAL", nullable: true),
                    releaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    publisherId = table.Column<int>(type: "INTEGER", nullable: false),
                    genreId = table.Column<int>(type: "INTEGER", nullable: false),
                    typeId = table.Column<int>(type: "INTEGER", nullable: false),
                    floor = table.Column<int>(type: "INTEGER", nullable: false),
                    alley = table.Column<int>(type: "INTEGER", nullable: false),
                    rowNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.bookId);
                    table.ForeignKey(
                        name: "FK_Book_BookType_typeId",
                        column: x => x.typeId,
                        principalTable: "BookType",
                        principalColumn: "typeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genre_genreId",
                        column: x => x.genreId,
                        principalTable: "Genre",
                        principalColumn: "genreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Publisher_publisherId",
                        column: x => x.publisherId,
                        principalTable: "Publisher",
                        principalColumn: "publisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeConfirmingPaymentsBook",
                columns: table => new
                {
                    employeeConfirmingPaymentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    employeeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeConfirmingPayments~", x => x.employeeConfirmingPaymentId);
                    table.ForeignKey(
                        name: "FK_EmployeeConfirmingPayments~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeConfirmingReturnsBook",
                columns: table => new
                {
                    employeeConfirmingReturnId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    employeeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeConfirmingReturnsB~", x => x.employeeConfirmingReturnId);
                    table.ForeignKey(
                        name: "FK_EmployeeConfirmingReturnsB~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeData",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    pesel = table.Column<long>(type: "INTEGER", nullable: false),
                    street = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    houseNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    town = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    zipCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    phoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    birthDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeData", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeData_Employee_empl~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomReservation",
                columns: table => new
                {
                    reservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    readerId = table.Column<int>(type: "INTEGER", nullable: false),
                    roomNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    begginingOfReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    endOfReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    employeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Confirmation = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservation", x => x.reservationId);
                    table.ForeignKey(
                        name: "FK_RoomReservation_Employee_e~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                    table.ForeignKey(
                        name: "FK_RoomReservation_Reader_rea~",
                        column: x => x.readerId,
                        principalTable: "Reader",
                        principalColumn: "readerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomReservation_Room_roomN~",
                        column: x => x.roomNumber,
                        principalTable: "Room",
                        principalColumn: "roomNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Author",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "INTEGER", nullable: false),
                    authorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Author", x => new { x.bookId, x.authorId });
                    table.ForeignKey(
                        name: "FK_Book_Author_Author_authorId",
                        column: x => x.authorId,
                        principalTable: "Author",
                        principalColumn: "authorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Author_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Opinions",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "INTEGER", nullable: false),
                    readerId = table.Column<int>(type: "INTEGER", nullable: false),
                    rating = table.Column<double>(type: "REAL", nullable: false),
                    addedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    opinion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Opinions", x => new { x.bookId, x.readerId });
                    table.ForeignKey(
                        name: "FK_Book_Opinions_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Opinions_Reader_reade~",
                        column: x => x.readerId,
                        principalTable: "Reader",
                        principalColumn: "readerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Tag",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "INTEGER", nullable: false),
                    tagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Tag", x => new { x.bookId, x.tagId });
                    table.ForeignKey(
                        name: "FK_Book_Tag_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Tag_Tag_tagId",
                        column: x => x.tagId,
                        principalTable: "Tag",
                        principalColumn: "tagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrowing",
                columns: table => new
                {
                    borrowId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    borrowDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    plannedReturnDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Confirmation = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsReturned = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    returnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    bookId = table.Column<int>(type: "INTEGER", nullable: false),
                    employeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    employeeConfirmingReturnId = table.Column<int>(type: "INTEGER", nullable: true),
                    employeeConfirmingPaymentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowing", x => x.borrowId);
                    table.ForeignKey(
                        name: "FK_Borrowing_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrowing_EmployeeConfirmi~",
                        column: x => x.employeeConfirmingPaymentId,
                        principalTable: "EmployeeConfirmingPaymentsBook",
                        principalColumn: "employeeConfirmingPaymentId");
                    table.ForeignKey(
                        name: "FK_Borrowing_EmployeeConfirm~1",
                        column: x => x.employeeConfirmingReturnId,
                        principalTable: "EmployeeConfirmingReturnsBook",
                        principalColumn: "employeeConfirmingReturnId");
                    table.ForeignKey(
                        name: "FK_Borrowing_Employee_employe~",
                        column: x => x.employeeId,
                        principalTable: "Employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "Reader_Borrowings",
                columns: table => new
                {
                    readerId = table.Column<int>(type: "INTEGER", nullable: false),
                    borrowId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader_Borrowings", x => new { x.readerId, x.borrowId });
                    table.ForeignKey(
                        name: "FK_Reader_Borrowings_Borrowin~",
                        column: x => x.borrowId,
                        principalTable: "Borrowing",
                        principalColumn: "borrowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reader_Borrowings_Reader_r~",
                        column: x => x.readerId,
                        principalTable: "Reader",
                        principalColumn: "readerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_genreId",
                table: "Book",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_publisherId",
                table: "Book",
                column: "publisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_typeId",
                table: "Book",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Author_authorId",
                table: "Book_Author",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Opinions_readerId",
                table: "Book_Opinions",
                column: "readerId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Tag_tagId",
                table: "Book_Tag",
                column: "tagId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_bookId",
                table: "Borrowing",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_employeeConfirm~1",
                table: "Borrowing",
                column: "employeeConfirmingReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_employeeConfirmi~",
                table: "Borrowing",
                column: "employeeConfirmingPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_employeeId",
                table: "Borrowing",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_positionId",
                table: "Employee",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeConfirmingPayments~",
                table: "EmployeeConfirmingPaymentsBook",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeConfirmingReturnsB~",
                table: "EmployeeConfirmingReturnsBook",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_Borrowings_borrowId",
                table: "Reader_Borrowings",
                column: "borrowId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_employeeId",
                table: "RoomReservation",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_readerId",
                table: "RoomReservation",
                column: "readerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservation_roomNumber",
                table: "RoomReservation",
                column: "roomNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Book_Author");

            migrationBuilder.DropTable(
                name: "Book_Opinions");

            migrationBuilder.DropTable(
                name: "Book_Tag");

            migrationBuilder.DropTable(
                name: "EmployeeData");

            migrationBuilder.DropTable(
                name: "Reader_Borrowings");

            migrationBuilder.DropTable(
                name: "RoomReservation");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Borrowing");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "EmployeeConfirmingPaymentsBook");

            migrationBuilder.DropTable(
                name: "EmployeeConfirmingReturnsBook");

            migrationBuilder.DropTable(
                name: "BookType");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Position");
        }
    }
}
