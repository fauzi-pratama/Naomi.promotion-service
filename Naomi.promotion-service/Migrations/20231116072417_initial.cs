using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Naomi.promotion_service.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promo_master",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    promo_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    qty = table.Column<int>(type: "integer", nullable: true),
                    qty_book = table.Column<int>(type: "integer", nullable: true),
                    balance = table.Column<decimal>(type: "numeric", nullable: true),
                    balance_book = table.Column<decimal>(type: "numeric", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_master_class",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promotion_class_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    promotion_class_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master_class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_master_mop",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    site_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mop_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mop_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master_mop", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_master_site",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    site_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    site_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master_site", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_master_type",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promotion_class_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    promotion_type_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    promotion_type_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_master_user_email",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master_user_email", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_master_zone",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    site_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pricing_zone_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pricing_zone_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master_zone", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_otp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_use = table.Column<bool>(type: "boolean", nullable: false),
                    exp_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_otp", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_trans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    trans_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    trans_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    commited = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_trans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_workflow",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_workflow", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_trans_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_trans_id = table.Column<Guid>(type: "uuid", nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    promo_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    promo_otp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    zone_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    site_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    member = table.Column<bool>(type: "boolean", nullable: false),
                    new_member = table.Column<bool>(type: "boolean", nullable: false),
                    member_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    promo_apps = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    qty_promo = table.Column<int>(type: "integer", nullable: true),
                    promo_total = table.Column<decimal>(type: "numeric", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_trans_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_trans_detail_promo_trans_promo_trans_id",
                        column: x => x.promo_trans_id,
                        principalTable: "promo_trans",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_workflow_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    redeem_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cls = table.Column<int>(type: "integer", nullable: false),
                    lvl = table.Column<int>(type: "integer", nullable: false),
                    item_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    result_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    promo_action_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    promo_action_value = table.Column<string>(type: "text", nullable: true),
                    max_value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    max_multiple = table.Column<int>(type: "integer", nullable: true),
                    max_use = table.Column<int>(type: "integer", nullable: true),
                    max_balance = table.Column<decimal>(type: "numeric", nullable: true),
                    max_discount = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ref_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    multiple_qty = table.Column<int>(type: "integer", nullable: false),
                    entertaiment_nip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    promo_desc = table.Column<string>(type: "text", nullable: true),
                    promo_term_condition = table.Column<string>(type: "text", nullable: true),
                    promo_show = table.Column<bool>(type: "boolean", nullable: false),
                    member = table.Column<bool>(type: "boolean", nullable: false),
                    new_member = table.Column<bool>(type: "boolean", nullable: false),
                    promo_image_link = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_promo_workflow_promo_workflow_id",
                        column: x => x.promo_workflow_id,
                        principalTable: "promo_workflow",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_workflow_expression",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_workflow_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    expression = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_workflow_expression", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_workflow_expression_promo_workflow_promo_workflow_id",
                        column: x => x.promo_workflow_id,
                        principalTable: "promo_workflow",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule_apps",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_rule_id = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule_apps", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_apps_promo_rule_promo_rule_id",
                        column: x => x.promo_rule_id,
                        principalTable: "promo_rule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule_expression",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_rule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    group_line = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    params_expression = table.Column<string>(type: "text", nullable: false),
                    link_exp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule_expression", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_expression_promo_rule_promo_rule_id",
                        column: x => x.promo_rule_id,
                        principalTable: "promo_rule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule_membership",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_rule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name_status = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule_membership", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_membership_promo_rule_promo_rule_id",
                        column: x => x.promo_rule_id,
                        principalTable: "promo_rule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule_mop",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_rule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    mop_promo_selection_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mop_promo_selection_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    mop_group_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mop_group_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule_mop", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_mop_promo_rule_promo_rule_id",
                        column: x => x.promo_rule_id,
                        principalTable: "promo_rule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule_result",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_rule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    group_line = table.Column<int>(type: "integer", nullable: false),
                    item = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dsc_value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    max_value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    link_rsl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_result_promo_rule_promo_rule_id",
                        column: x => x.promo_rule_id,
                        principalTable: "promo_rule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "promo_rule_variable",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_rule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    params_expression = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_rule_variable", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_rule_variable_promo_rule_promo_rule_id",
                        column: x => x.promo_rule_id,
                        principalTable: "promo_rule",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "promo_master_class",
                columns: new[] { "id", "active_flag", "created_by", "created_date", "line_num", "promotion_class_key", "promotion_class_name", "updated_by", "updated_date" },
                values: new object[,]
                {
                    { new Guid("302be9cd-5e08-454d-b8e5-582d336750d7"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1578), 1, "ITEM", "ITEM", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1588) },
                    { new Guid("8713bd36-48d6-43dd-94b9-407c3aff1528"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1592), 2, "CART", "CART", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1592) },
                    { new Guid("c386c5f1-d3d2-4e7f-ad6a-34b4f185325c"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1596), 4, "Entertain", "Entertain", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1597) },
                    { new Guid("dbf358cb-f43b-4d69-9176-8ee63ac8953f"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1594), 3, "MOP", "MOP", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1594) }
                });

            migrationBuilder.InsertData(
                table: "promo_master_type",
                columns: new[] { "id", "active_flag", "created_by", "created_date", "line_num", "promotion_class_id", "promotion_type_key", "promotion_type_name", "updated_by", "updated_date" },
                values: new object[,]
                {
                    { new Guid("1f57489b-cca0-4392-ae00-3d145012d375"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1724), 2, "302BE9CD-5E08-454D-B8E5-582D336750D7", "AMOUNT", "DISCOUNT AMOUNT ITEM", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1725) },
                    { new Guid("2524251a-565a-46c0-93d5-deea80c63ff5"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1733), 2, "8713BD36-48D6-43DD-94B9-407C3AFF1528", "PERCENT", "DISCOUNT PERCENT CART", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1733) },
                    { new Guid("3c7ed57d-8235-453f-8f97-ba93b3747b4f"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1735), 2, "DBF358CB-F43B-4D69-9176-8EE63AC8953F", "AMOUNT", "DISCOUNT AMOUNT MOP", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1735) },
                    { new Guid("57ae0d50-1d3b-4a33-8d7c-a4cab863aa30"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1738), 2, "C386C5F1-D3D2-4E7F-AD6A-34B4F185325C", "PERCENT", "DISCOUNT PERCENT Entertain", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1739) },
                    { new Guid("86ed449a-e4bc-4c28-a6e5-3ba18e491e63"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1730), 2, "8713BD36-48D6-43DD-94B9-407C3AFF1528", "AMOUNT", "DISCOUNT AMOUNT CART", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1731) },
                    { new Guid("886470d3-5e0b-41ed-baa7-10cd94511e10"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1726), 2, "302BE9CD-5E08-454D-B8E5-582D336750D7", "PERCENT", "DISCOUNT PERCENT ITEM", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1727) },
                    { new Guid("bd4f0c46-7d03-45fa-b33c-77028218593a"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1728), 2, "302BE9CD-5E08-454D-B8E5-582D336750D7", "BUNDLE", "DISCOUNT BUNDLING ITEM", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1729) },
                    { new Guid("dda43968-95bd-4d94-8737-fd621d0a5895"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1736), 2, "DBF358CB-F43B-4D69-9176-8EE63AC8953F", "PERCENT", "DISCOUNT PERCENT MOP", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1737) },
                    { new Guid("e0d70f81-6a25-434d-9055-e50554ef585c"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1722), 1, "302BE9CD-5E08-454D-B8E5-582D336750D7", "SP", "SPECIAL PRICE ITEM", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1723) },
                    { new Guid("fac8e236-2fb7-4b4a-b644-0680f60fd0a0"), true, "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1719), 1, "302BE9CD-5E08-454D-B8E5-582D336750D7", "ITEM", "BUY X GET Y ITEM", "System", new DateTime(2023, 11, 16, 14, 24, 17, 134, DateTimeKind.Local).AddTicks(1720) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_promo_workflow_id",
                table: "promo_rule",
                column: "promo_workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_apps_promo_rule_id",
                table: "promo_rule_apps",
                column: "promo_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_expression_promo_rule_id",
                table: "promo_rule_expression",
                column: "promo_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_membership_promo_rule_id",
                table: "promo_rule_membership",
                column: "promo_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_mop_promo_rule_id",
                table: "promo_rule_mop",
                column: "promo_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_result_promo_rule_id",
                table: "promo_rule_result",
                column: "promo_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_rule_variable_promo_rule_id",
                table: "promo_rule_variable",
                column: "promo_rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_trans_detail_promo_trans_id",
                table: "promo_trans_detail",
                column: "promo_trans_id");

            migrationBuilder.CreateIndex(
                name: "IX_promo_workflow_expression_promo_workflow_id",
                table: "promo_workflow_expression",
                column: "promo_workflow_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promo_master");

            migrationBuilder.DropTable(
                name: "promo_master_class");

            migrationBuilder.DropTable(
                name: "promo_master_mop");

            migrationBuilder.DropTable(
                name: "promo_master_site");

            migrationBuilder.DropTable(
                name: "promo_master_type");

            migrationBuilder.DropTable(
                name: "promo_master_user_email");

            migrationBuilder.DropTable(
                name: "promo_master_zone");

            migrationBuilder.DropTable(
                name: "promo_otp");

            migrationBuilder.DropTable(
                name: "promo_rule_apps");

            migrationBuilder.DropTable(
                name: "promo_rule_expression");

            migrationBuilder.DropTable(
                name: "promo_rule_membership");

            migrationBuilder.DropTable(
                name: "promo_rule_mop");

            migrationBuilder.DropTable(
                name: "promo_rule_result");

            migrationBuilder.DropTable(
                name: "promo_rule_variable");

            migrationBuilder.DropTable(
                name: "promo_trans_detail");

            migrationBuilder.DropTable(
                name: "promo_workflow_expression");

            migrationBuilder.DropTable(
                name: "promo_rule");

            migrationBuilder.DropTable(
                name: "promo_trans");

            migrationBuilder.DropTable(
                name: "promo_workflow");
        }
    }
}
