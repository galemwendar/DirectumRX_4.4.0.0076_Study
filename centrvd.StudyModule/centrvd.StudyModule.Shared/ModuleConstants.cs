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
      
    }
    public static class DocumentKindNames
    {
      [Public]
      public const string SuppliesAllocationContract = "Договор размещения ресурсов";
      public const string SuppliesAllocationContractShort = "Договор размещения ресурсов";
      
      [Public]
      public const string SuppliesLeaseContract = "Договор передачи ресурсов в аренду";
      public const string SuppliesLeaseContractShort = "Договор передачи ресурсов в аренду";
    }


  }
}