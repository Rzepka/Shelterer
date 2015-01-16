using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Device.Location;
using SheltererExtensionMethods;
using System.Threading.Tasks;

namespace Shelterer.Models
{
//    public class SheltersInitializer : DropCreateDatabaseAlways<SheltersContext>
    public class SheltersInitializer : DropCreateDatabaseIfModelChanges<SheltersContext>
    {
        protected override void Seed(SheltersContext context)
        {
            var regions = new List<Region>
            {
                new Region{RegionName="Beskid Żywiecki"},
                new Region{RegionName="Beskid Śląski"},
            };
            foreach(var s in regions)
            {
                context.Regions.Add(s);
                context.SaveChanges();
                context.SaveRecordSync(s, "user", "New added");
            }
            

            var mountainRanges = new List<MountainRange>
            {
                new MountainRange{MountainRangeName="Grupa Wielkiej Raczy",Region=regions[0]},
            };
            foreach(var s in mountainRanges)
            {
                context.MountainRanges.Add(s);
                context.SaveChanges();
                context.SaveRecordSync(s, "user", "New added");
            }

            var objectTypes = new List<ObjectType>
            {
                new ObjectType{ObjectTypeName="bacówka"},
                new ObjectType{ObjectTypeName="baza namiotowa"},
            };
            foreach(var s in objectTypes)
            {
                context.ObjectTypes.Add(s);
                context.SaveChanges();
                context.SaveRecordSync(s, "user", "New added");
            }

            var shelters = new List<Shelter>
            {
                new Shelter {
                    Name = "Bryzgałki",
                    ObjectType = objectTypes[0],
                    Altitude = 1200,
                    Latitude = 49.382198,
                    Longitude = 19.006674,
                    Region = regions[0],
                    MountainRange = mountainRanges[0],
                    Visited = true,
                    Capacity = 20,
                    Opening = "2 budynki otwarte, reszta zamknięta na kłódki.",
                    Location = "Na przełęczy między Bugajem a Jaworzyną z czerwonego szlaku dojść do przecinki na granicy. 2 m przed słupkiem nr. 55 wejść w las wąską ścieżką (nie drogą). Ścieżka trawersuje od zachodu Javorine. Dojście do bacówek zajmuje około 20 minut.",
                    TechnicalCondition = "Budynki w bardzo dobrym stanie, piece w środku nieszczelne.",
                    Remarks = "",
                    WaterAccess = "Po drugiej stronie drogi, na przeciw wejścia do budynku nr.8.",
                    Fireplace = "Kilka miejsc na ogniska przed budynkami, wewnątrz nieszczelne piece.",
                },
                new Shelter {
                    Name = "Przysłop Potócki",
                    ObjectType = objectTypes[0],
                    Altitude = 880,
                    Latitude = 49.4423118,
                    Longitude = 19.0441694,
                    Region = regions[0],
                    MountainRange = mountainRanges[0],
                    Visited = true,
                    Capacity = 10,
                    Owner = "PTTK PolSl",
                    Opening = "2 budynki otwarte wrzesień-czerwiec, w okresie wakacyjnym funkcjonuje jako baza namiotowa",
                    Location = "zaraz przy czarnym szlaku z Przegibka do Soli. Na przełęczy Przysłop Potócki między Bednoszką Wielką a Parszywką Dużą.",
                    TechnicalCondition = "Główny budynek bazy w bardzo dobrym stanie, budynek z jadalnią nieco przewiewny.",
                    Remarks = "Szczegóły: http://przyslop-potocki.pttk.pl/",
                    WaterAccess = "Dostępne źródło",
                    Fireplace = "Miejsce na ognisko przed głównym budynkiem.",
                },
            };
            foreach(var s in shelters)
            {
                context.Shelters.Add(s);
                context.SaveChanges();
                context.SaveRecordSync(s, "user", "New added");
            }
            if (File.Exists("C:\\ImagesShelterer\\DSC_0220.JPG")
                && File.Exists("C:\\ImagesShelterer\\10756.jpg"))
            {
                var images = new List<Image>
                {
                    new Image
                    {
                        ShelterId = shelters[0].Id,
                        Name = "DSC_0220.JPG",
                        Type = "image/jpeg",
                        Size = 483667,
                        FileContent = File.ReadAllBytes("C:\\ImagesShelterer\\DSC_0220.JPG"),
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                    },
                    new Image
                    {
                        ShelterId = shelters[1].Id,
                        Name = "10756.jpg",
                        Type = "image/jpeg",
                        Size = 437484,
                        FileContent = File.ReadAllBytes("C:\\ImagesShelterer\\10756.jpg"),
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                    }
                };
                foreach (var s in images)
                {
                    context.Images.Add(s);
                    context.SaveChanges();
                    context.SaveRecordSync(s, "user", "Image added");
                }
            }
        }
    }
}