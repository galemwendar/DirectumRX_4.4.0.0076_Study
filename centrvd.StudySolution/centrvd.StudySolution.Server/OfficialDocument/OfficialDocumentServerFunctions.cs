using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.OfficialDocument;

namespace centrvd.StudySolution.Server
{
  partial class OfficialDocumentFunctions
  {
    [Public]
    public static  List<Sungero.Domain.Shared.IEntityInfo> GetTypesAviableForChange()
    {
      //TODO это не работает
      try
      {
        var infos = Sungero.Docflow.ContractualDocumentBases.GetAll()
          .Select(c => c.Info)
          .Distinct()
          .ToList();
        
        return infos.Cast<Sungero.Domain.Shared.IEntityInfo>().ToList();
      }
      catch
      {
        Logger.ErrorFormat( "Error cast ContractualDocumentBaseinfo to Sungero.Domain.Shared.IEntityInfo");
        return null;
      }

    }
  }
}