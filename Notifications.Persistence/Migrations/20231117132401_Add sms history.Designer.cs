﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notifications.Persistence.DataContext;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notifications.Persistence.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20231117132401_Add sms history")]
    partial class Addsmshistory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notifications.Domain.Entities.NotificationHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("NotificationHistory");

                    b.HasDiscriminator<string>("Discriminator").HasValue("NotificationHistory");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notifications.Domain.Entities.NotificationTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(129536)
                        .HasColumnType("character varying(129536)");

                    b.Property<int>("TemplateType")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("NotificationTemplates", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notifications.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserSettingsId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserSettingsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Notifications.Domain.Entities.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("PreferredNotificationType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Notifications.Domain.Entities.EmailHistory", b =>
                {
                    b.HasBaseType("Notifications.Domain.Entities.NotificationHistory");

                    b.Property<string>("ReceiverEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("SenderEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("EmailHistory");
                });

            modelBuilder.Entity("Notifications.Domain.Entities.SmsHistory", b =>
                {
                    b.HasBaseType("Notifications.Domain.Entities.NotificationHistory");

                    b.Property<string>("ReceiverPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("SenderPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasDiscriminator().HasValue("SmsHistory");
                });

            modelBuilder.Entity("Notifications.Domain.Entities.EmailTemplate", b =>
                {
                    b.HasBaseType("Notifications.Domain.Entities.NotificationTemplate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Notifications.Domain.Entities.SmsTemplate", b =>
                {
                    b.HasBaseType("Notifications.Domain.Entities.NotificationTemplate");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Notifications.Domain.Entities.NotificationHistory", b =>
                {
                    b.HasOne("Notifications.Domain.Entities.NotificationTemplate", "Template")
                        .WithMany("Histories")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Notifications.Domain.Entities.User", b =>
                {
                    b.HasOne("Notifications.Domain.Entities.UserSettings", "UserSettings")
                        .WithMany()
                        .HasForeignKey("UserSettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSettings");
                });

            modelBuilder.Entity("Notifications.Domain.Entities.NotificationTemplate", b =>
                {
                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
