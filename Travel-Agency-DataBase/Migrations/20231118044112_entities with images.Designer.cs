﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Travel_Agency_DataBase;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    [DbContext(typeof(TravelAgencyContext))]
    [Migration("20231118044112_entities with images")]
    partial class entitieswithimages
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ExcursionTouristActivity", b =>
                {
                    b.Property<int>("ActivitiesId")
                        .HasColumnType("int");

                    b.Property<int>("ExcursionsId")
                        .HasColumnType("int");

                    b.HasKey("ActivitiesId", "ExcursionsId");

                    b.HasIndex("ExcursionsId");

                    b.ToTable("ExcursionTouristActivity");
                });

            modelBuilder.Entity("ExcursionTouristPlace", b =>
                {
                    b.Property<int>("ExcursionsId")
                        .HasColumnType("int");

                    b.Property<int>("PlacesId")
                        .HasColumnType("int");

                    b.HasKey("ExcursionsId", "PlacesId");

                    b.HasIndex("PlacesId");

                    b.ToTable("ExcursionTouristPlace");
                });

            modelBuilder.Entity("Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("OfferPackage", b =>
                {
                    b.Property<int>("OffersId")
                        .HasColumnType("int");

                    b.Property<int>("PackagesId")
                        .HasColumnType("int");

                    b.HasKey("OffersId", "PackagesId");

                    b.HasIndex("PackagesId");

                    b.ToTable("OfferPackage");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("FaxNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AgencyId")
                        .HasColumnType("int");

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("EndDate")
                        .HasColumnType("bigint");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<long>("StartDate")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.ToTable("Offers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Offer");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Packages.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Discount")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Payments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Payment");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.Reserve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PackageId");

                    b.ToTable("Reserves");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Reserve");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Excursion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOverNight")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Excursions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Excursion");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<long>("Duration")
                        .HasColumnType("bigint");

                    b.Property<int>("FlightCategory")
                        .HasColumnType("int");

                    b.Property<int>("OriginId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("OriginId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TouristPlaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TouristPlaceId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.TouristActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TouristActivities");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.TouristPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TouristPlaces");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.ExcursionOffer", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Offers.Offer");

                    b.Property<int>("ExcursionId")
                        .HasColumnType("int");

                    b.Property<string>("Facilities")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasIndex("ExcursionId");

                    b.ToTable("Offers", t =>
                        {
                            t.Property("Facilities")
                                .HasColumnName("ExcursionOffer_Facilities");
                        });

                    b.HasDiscriminator().HasValue("ExcursionOffer");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.FlightOffer", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Offers.Offer");

                    b.Property<string>("Facilities")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.HasIndex("FlightId");

                    b.ToTable("Offers", t =>
                        {
                            t.Property("Facilities")
                                .HasColumnName("FlightOffer_Facilities");
                        });

                    b.HasDiscriminator().HasValue("FlightOffer");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.HotelOffer", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Offers.Offer");

                    b.Property<string>("Facilities")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasIndex("HotelId");

                    b.HasDiscriminator().HasValue("HotelOffer");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.PaymentOnline", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Payments.Payment");

                    b.Property<long>("CreditCard")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("PaymentOnline");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.PaymentTicket", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Payments.Payment");

                    b.HasDiscriminator().HasValue("PaymentTicket");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.ReserveTicket", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Payments.Reserve");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasIndex("PaymentId");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("ReserveTicket");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.ReserveTourist", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Payments.Reserve");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasIndex("PaymentId");

                    b.HasIndex("UserId");

                    b.ToTable("Reserves", t =>
                        {
                            t.Property("PaymentId")
                                .HasColumnName("ReserveTourist_PaymentId");
                        });

                    b.HasDiscriminator().HasValue("ReserveTourist");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.OverNightExcursion", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Services.Excursion");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasIndex("HotelId");

                    b.HasDiscriminator().HasValue("OverNightExcursion");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Users.Tourist", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Users.User");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("Tourist");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Users.UserAgency", b =>
                {
                    b.HasBaseType("Travel_Agency_Domain.Users.User");

                    b.Property<int>("AgencyId")
                        .HasColumnType("int");

                    b.HasIndex("AgencyId");

                    b.HasDiscriminator().HasValue("UserAgency");
                });

            modelBuilder.Entity("ExcursionTouristActivity", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.TouristActivity", null)
                        .WithMany()
                        .HasForeignKey("ActivitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Services.Excursion", null)
                        .WithMany()
                        .HasForeignKey("ExcursionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExcursionTouristPlace", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.Excursion", null)
                        .WithMany()
                        .HasForeignKey("ExcursionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Services.TouristPlace", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OfferPackage", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Offers.Offer", null)
                        .WithMany()
                        .HasForeignKey("OffersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Packages.Package", null)
                        .WithMany()
                        .HasForeignKey("PackagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.Offer", b =>
                {
                    b.HasOne("Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.Payment", b =>
                {
                    b.OwnsOne("Travel_Agency_Domain.Others.UserIdentity", "UserIdentity", b1 =>
                        {
                            b1.Property<int>("PaymentId")
                                .HasColumnType("int");

                            b1.Property<string>("IdentityDocument")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("PaymentId");

                            b1.ToTable("Payments");

                            b1.WithOwner()
                                .HasForeignKey("PaymentId");
                        });

                    b.Navigation("UserIdentity")
                        .IsRequired();
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.Reserve", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Packages.Package", "Package")
                        .WithMany("Reserves")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Travel_Agency_Domain.Others.UserIdentity", "UserIdentities", b1 =>
                        {
                            b1.Property<int>("ReserveId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("IdentityDocument")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("ReserveId", "Id");

                            b1.ToTable("Reserves_UserIdentities");

                            b1.WithOwner()
                                .HasForeignKey("ReserveId");
                        });

                    b.Navigation("Package");

                    b.Navigation("UserIdentities");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Excursion", b =>
                {
                    b.HasOne("Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Flight", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.TouristPlace", "Destination")
                        .WithMany("DestinationFlights")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Services.TouristPlace", "Origin")
                        .WithMany("OriginFlights")
                        .HasForeignKey("OriginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Hotel", b =>
                {
                    b.HasOne("Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Services.TouristPlace", "TouristPlace")
                        .WithMany("Hotels")
                        .HasForeignKey("TouristPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("TouristPlace");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.TouristActivity", b =>
                {
                    b.HasOne("Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.TouristPlace", b =>
                {
                    b.HasOne("Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Travel_Agency_Domain.Others.Address", "Address", b1 =>
                        {
                            b1.Property<int>("TouristPlaceId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("TouristPlaceId");

                            b1.ToTable("TouristPlaces");

                            b1.WithOwner()
                                .HasForeignKey("TouristPlaceId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.ExcursionOffer", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.Excursion", "Excursion")
                        .WithMany("Offers")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Excursion");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.FlightOffer", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.Flight", "Flight")
                        .WithMany("Offers")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Offers.HotelOffer", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.Hotel", "Hotel")
                        .WithMany("Offers")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.ReserveTicket", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Payments.PaymentTicket", "Payment")
                        .WithMany("ReserveTickets")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Users.UserAgency", "User")
                        .WithMany("Reserves")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.ReserveTourist", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Payments.PaymentOnline", "Payment")
                        .WithMany("ReserveTourists")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Domain.Users.Tourist", "User")
                        .WithMany("Reserves")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.OverNightExcursion", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Services.Hotel", "Hotel")
                        .WithMany("OverNightExcursions")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Users.UserAgency", b =>
                {
                    b.HasOne("Travel_Agency_Domain.Agency", "Agency")
                        .WithMany("Users")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agency");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Agency", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Packages.Package", b =>
                {
                    b.Navigation("Reserves");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Excursion", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Flight", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.Hotel", b =>
                {
                    b.Navigation("Offers");

                    b.Navigation("OverNightExcursions");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Services.TouristPlace", b =>
                {
                    b.Navigation("DestinationFlights");

                    b.Navigation("Hotels");

                    b.Navigation("OriginFlights");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.PaymentOnline", b =>
                {
                    b.Navigation("ReserveTourists");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Payments.PaymentTicket", b =>
                {
                    b.Navigation("ReserveTickets");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Users.Tourist", b =>
                {
                    b.Navigation("Reserves");
                });

            modelBuilder.Entity("Travel_Agency_Domain.Users.UserAgency", b =>
                {
                    b.Navigation("Reserves");
                });
#pragma warning restore 612, 618
        }
    }
}
