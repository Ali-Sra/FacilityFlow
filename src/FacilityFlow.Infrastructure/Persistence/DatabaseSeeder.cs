using FacilityFlow.Domain.Entities;
using FacilityFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
namespace FacilityFlow.Infrastructure.Persistence;
public static class DatabaseSeeder
{
 public static async Task SeedAsync(FacilityFlowDbContext db,CancellationToken ct=default)
 {
  await db.Database.EnsureCreatedAsync(ct);if(await db.Buildings.AnyAsync(ct))return;
  var buildings=new[]{("Gebäude Nord","NORD","Musterweg 12","Beispielstadt","20001"),("Gebäude Süd","SUED","Demostraße 8","Beispielstadt","20002"),("Hauptgebäude","HAUPT","Testallee 30","Beispielstadt","20003"),("Schulungszentrum","SCHUL","Lernweg 4","Beispielstadt","20004"),("Lager West","WEST","Logistikring 7","Beispielstadt","20005"),("Technikzentrum","TECH","Innovationsplatz 1","Beispielstadt","20006")}.Select(x=>new Building{Name=x.Item1,Code=x.Item2,Address=x.Item3,City=x.Item4,PostalCode=x.Item5}).ToList();
  foreach(var b in buildings)for(var i=1;i<=5;i++)b.Rooms.Add(new Room{RoomNumber=$"{(i+1)*100}",Floor=$"{i-1}. OG",Description=$"Demoraum {i}"});db.AddRange(buildings);await db.SaveChangesAsync(ct);
  var rnd=new Random(42);var titles=new[]{"Netzwerkverbindung unterbrochen","Beleuchtung funktioniert nicht","Heizung bleibt kalt","Wasserhahn ist undicht","Aufzug meldet Störung","Arbeitsplatzrechner startet nicht","Türschließer defekt","Reinigung erforderlich","Klimaanlage ungewöhnlich laut","Monitor zeigt kein Bild"};var technicians=new[]{"Anna Becker","Daniel Krüger","Lea Hoffmann","Mehmet Yilmaz","Sophie Wagner"};
  for(var i=1;i<=60;i++){var b=buildings[rnd.Next(buildings.Count)];var room=b.Rooms.ElementAt(rnd.Next(b.Rooms.Count));var status=(ServiceRequestStatus)rnd.Next(0,6);var priority=(Priority)rnd.Next(0,4);var created=DateTime.UtcNow.AddDays(-rnd.Next(0,90));db.ServiceRequests.Add(new ServiceRequest{TicketNumber=$"SR-2026-{i:000000}",Title=titles[rnd.Next(titles.Length)],Description="Synthetische Demoanfrage zur Darstellung eines realistischen Bearbeitungsprozesses.",Category=(ServiceCategory)rnd.Next(0,11),Priority=priority,Status=status,BuildingId=b.Id,RoomId=room.Id,AssignedTechnician=rnd.NextDouble()>.25?technicians[rnd.Next(technicians.Length)]:null,CreatedAtUtc=created,DueDateUtc=created.AddDays(priority==Priority.Kritisch?1:priority==Priority.Hoch?2:priority==Priority.Mittel?4:7),ResolvedAtUtc=status is ServiceRequestStatus.Geloest or ServiceRequestStatus.Geschlossen?created.AddDays(rnd.Next(1,6)):null,ResolutionDescription=status is ServiceRequestStatus.Geloest or ServiceRequestStatus.Geschlossen?"Die Ursache wurde geprüft und fachgerecht behoben.":null});}
  await db.SaveChangesAsync(ct);
 }
}
