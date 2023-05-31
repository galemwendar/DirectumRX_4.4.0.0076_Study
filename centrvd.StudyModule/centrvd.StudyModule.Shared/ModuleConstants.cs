using System;
using Sungero.Core;

namespace centrvd.StudyModule.Constants
{
  public static class Module
  {
    public static class DocumentKindGuid
    {
      [Public]
      public static readonly Guid SuppliesAllocationContract = Guid.Parse("309212B3-0589-4F13-96CD-AACD7A05F9E6");
      
      [Public]
      public static readonly Guid SuppliesLeaseContract = Guid.Parse("3DEF197B-A9EE-4A1C-A0BA-B996739C632F");
      
 [Public]
      public static readonly Guid GrantingAcessMemo= Guid.Parse("CD4A7429-993A-4D6F-9E87-AC2BD13E5D57");
      
      [Public]
      public static readonly Guid MeetingMemo = Guid.Parse("3D4952D6-151F-403D-BEB3-CC684F978869");
    }

    
    public static class DocumentKindNames
    {
      [Public]
      public const string SuppliesAllocationContract = "Договор размещения ресурсов";
      public const string SuppliesAllocationContractShort = "Договор размещения ресурсов";
      
      [Public]
      public const string SuppliesLeaseContract = "Договор передачи ресурсов в аренду";
      public const string SuppliesLeaseContractShort = "Договор передачи ресурсов в аренду";
    [Public]
      public const string GrantingAcessMemo = "Служебная записка на предоставление доступа";
      
      public const string GrantingAcessMemoShort = "Служебная записка на предоставление доступа";
      
      [Public]
      public const string MeetingMemo = "Служебная записка о проведении совещания";
      
      public const string MeetingMemoShort = "Служебная записка о проведении совещания";
    }


  }
}
