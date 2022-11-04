﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OmahaDotDev.ResourceAccess.Database;

#nullable disable

namespace OmahaDotDev.ResourceAccess.Database.Migrations
{
    [DbContext(typeof(SiteDbContext))]
    partial class SiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("groups")
                .HasAnnotation("ProductVersion", "7.0.0-rc.2.22472.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.GroupDomainNameRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("GroupDomainNames", "groups");
                });

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.GroupRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("Groups", "groups");
                });

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.MemberRecord", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.ToTable("Members", "groups");

                    b.HasData(
                        new
                        {
                            UserId = "1"
                        });
                });

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.GroupDomainNameRecord", b =>
                {
                    b.HasOne("OmahaDotDev.ResourceAccess.Database.Model.MemberRecord", "CreatedByUser")
                        .WithMany("CreatedGroupDomainNames")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OmahaDotDev.ResourceAccess.Database.Model.GroupRecord", "Group")
                        .WithMany("DomainNames")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OmahaDotDev.ResourceAccess.Database.Model.MemberRecord", "UpdatedByUser")
                        .WithMany("UpdatedGroupDomainNames")
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Group");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.GroupRecord", b =>
                {
                    b.HasOne("OmahaDotDev.ResourceAccess.Database.Model.MemberRecord", "CreatedByUser")
                        .WithMany("CreatedGroups")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OmahaDotDev.ResourceAccess.Database.Model.MemberRecord", "UpdatedByUser")
                        .WithMany("UpdatedGroups")
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.GroupRecord", b =>
                {
                    b.Navigation("DomainNames");
                });

            modelBuilder.Entity("OmahaDotDev.ResourceAccess.Database.Model.MemberRecord", b =>
                {
                    b.Navigation("CreatedGroupDomainNames");

                    b.Navigation("CreatedGroups");

                    b.Navigation("UpdatedGroupDomainNames");

                    b.Navigation("UpdatedGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
