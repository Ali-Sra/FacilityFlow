using FacilityFlow.Domain.Enums;
namespace FacilityFlow.Domain.Rules;
public static class StatusTransitionRules
{
    private static readonly IReadOnlyDictionary<ServiceRequestStatus, ServiceRequestStatus[]> Allowed =
        new Dictionary<ServiceRequestStatus, ServiceRequestStatus[]>
        {
            [ServiceRequestStatus.Neu] = [ServiceRequestStatus.Zugewiesen, ServiceRequestStatus.Abgelehnt],
            [ServiceRequestStatus.Zugewiesen] = [ServiceRequestStatus.InBearbeitung, ServiceRequestStatus.Abgelehnt],
            [ServiceRequestStatus.InBearbeitung] = [ServiceRequestStatus.WartetAufMaterial, ServiceRequestStatus.WartetAufRueckmeldung, ServiceRequestStatus.Geloest],
            [ServiceRequestStatus.WartetAufMaterial] = [ServiceRequestStatus.InBearbeitung],
            [ServiceRequestStatus.WartetAufRueckmeldung] = [ServiceRequestStatus.InBearbeitung],
            [ServiceRequestStatus.Geloest] = [ServiceRequestStatus.Geschlossen, ServiceRequestStatus.InBearbeitung],
            [ServiceRequestStatus.Geschlossen] = [],
            [ServiceRequestStatus.Abgelehnt] = []
        };
    public static bool IsAllowed(ServiceRequestStatus from, ServiceRequestStatus to) =>
        from == to || (Allowed.TryGetValue(from, out var targets) && targets.Contains(to));
}
