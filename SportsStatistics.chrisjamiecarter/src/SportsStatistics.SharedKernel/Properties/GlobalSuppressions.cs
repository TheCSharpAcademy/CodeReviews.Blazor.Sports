// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "This type will not be exposed to external consumers.", Scope = "type", Target = "~T:SportsStatistics.SharedKernel.Error")]
[assembly: SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "This type will not be exposed to external consumers.", Scope = "type", Target = "~T:SportsStatistics.SharedKernel.IDomainEventHandler`1")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Enumeration pattern requires per-closed-type static members.", Scope = "type", Target = "~T:SportsStatistics.SharedKernel.Enumeration`1")]
