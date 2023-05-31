using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.OfficialDocument;

namespace centrvd.StudySolution.Client
{
  partial class OfficialDocumentFunctions
  {
    [Public]
    public override Sungero.Docflow.IOfficialDocument ChangeDocumentType(List<Sungero.Domain.Shared.IEntityInfo> types)
    { 
      Sungero.Docflow.IOfficialDocument convertedDoc = null;
      
      // Запретить смену типа, если документ или его тело заблокировано.
      var isCalledByDocument = CallContext.CalledDirectlyFrom(OfficialDocuments.Info);
      if (isCalledByDocument && Sungero.Docflow.PublicFunctions.Module.IsLockedByOther(_obj) ||
          !isCalledByDocument && Sungero.Docflow.PublicFunctions.Module.IsLocked(_obj) ||
          Sungero.Docflow.PublicFunctions.Module.VersionIsLocked(_obj.Versions.ToList()))
      {
        Dialogs.ShowMessage(Sungero.Docflow.ExchangeDocuments.Resources.ChangeDocumentTypeLockError,
                            MessageType.Error);
        return convertedDoc;
      }
      
      // Открыть диалог по смене типа.
      var title = Sungero.Docflow.ExchangeDocuments.Resources.TypeChange;
      var dialog = Dialogs.CreateSelectTypeDialog(title, types.ToArray());
      if (dialog.Show() == DialogButtons.Ok)
        convertedDoc = OfficialDocuments.As(_obj.ConvertTo(dialog.SelectedType));
      
      return convertedDoc;
    }
    [Public]
    public override List<Sungero.Domain.Shared.IEntityInfo> GetTypesAvailableForChange()
    {
      var types = new List<Sungero.Domain.Shared.IEntityInfo>();
      types.Add(Sungero.FinancialArchive.ContractStatements.Info);
      types.Add(Sungero.FinancialArchive.OutgoingTaxInvoices.Info);
      types.Add(Sungero.Contracts.ContractualDocuments.Info);
      types.Add(centrvd.StudyModule.SuppliesAllocationContracts.Info);
      types.Add(centrvd.StudyModule.SuppliesLeaseContracts.Info);
      types.Add(Sungero.FinancialArchive.Waybills.Info);
      types.Add(Sungero.Docflow.CounterpartyDocuments.Info);
      return types;
    }
  }
}

