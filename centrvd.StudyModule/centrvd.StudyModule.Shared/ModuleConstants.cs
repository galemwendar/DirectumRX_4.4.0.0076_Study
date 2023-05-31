using System;
using Sungero.Core;

namespace centrvd.StudyModule.Constants
{
  public static class Module
  {
    public static class DocumentKindGuid
    {
      [Public]
      public static readonly Guid GrantingAcessMemo= Guid.Parse("CD4A7429-993A-4D6F-9E87-AC2BD13E5D57");
      
      [Public]
      public static readonly Guid MeetingMemo = Guid.Parse("3D4952D6-151F-403D-BEB3-CC684F978869");
    }

    
    public static class DocumentKindNames
    {
      [Public]
      public const string GrantingAcessMemo = "Служебная записка на предоставление доступа";
      
      public const string GrantingAcessMemoShort = "Служебная записка на предоставление доступа";
      
      [Public]
      public const string MeetingMemo = "Служебная записка о проведении совещания";
      
      public const string MeetingMemoShort = "Служебная записка о проведении совещания";
    }
  }
}