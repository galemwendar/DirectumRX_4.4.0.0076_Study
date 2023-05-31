using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace centrvd.StudyModule.Client
{
  public class ModuleFunctions
  {

    /// <summary>
    /// GetAllInfos
    /// </summary>
    public virtual void GetAllInfos()
    {
      var list = new List<string>();
      //работет
      var contracts = Sungero.Docflow.ContractualDocumentBases.GetAll().ToList();
      foreach (var element in contracts)
      {
        list.Add(element.Info.Name);
      }
      
      //не работает
      var contracts1 = SuppliesContracts.GetAll().ToList();
      var infos = contracts1.Select(c => c.Info);
      foreach (var element in infos)
      {
        var info1 = element;
      }
      
    }

  }
}