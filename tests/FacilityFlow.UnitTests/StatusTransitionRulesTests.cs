using FacilityFlow.Domain.Enums;
using FacilityFlow.Domain.Rules;
namespace FacilityFlow.UnitTests;
public sealed class StatusTransitionRulesTests
{
 [Theory][InlineData(ServiceRequestStatus.Neu,ServiceRequestStatus.Zugewiesen)][InlineData(ServiceRequestStatus.InBearbeitung,ServiceRequestStatus.Geloest)][InlineData(ServiceRequestStatus.Geloest,ServiceRequestStatus.Geschlossen)]
 public void IsAllowed_WithValidTransition_ReturnsTrue(ServiceRequestStatus from,ServiceRequestStatus to)=>Assert.True(StatusTransitionRules.IsAllowed(from,to));
 [Theory][InlineData(ServiceRequestStatus.Neu,ServiceRequestStatus.Geschlossen)][InlineData(ServiceRequestStatus.Geschlossen,ServiceRequestStatus.InBearbeitung)]
 public void IsAllowed_WithInvalidTransition_ReturnsFalse(ServiceRequestStatus from,ServiceRequestStatus to)=>Assert.False(StatusTransitionRules.IsAllowed(from,to));
}
