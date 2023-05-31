using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudyModule.SuppliesContract;

namespace centrvd.StudyModule.Client
{
  partial class SuppliesContractActions
  {
    public override void ChangeDocumentType(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      // Для смены типа необходимо отменить регистрацию.
      if (_obj.RegistrationState == SuppliesContract.RegistrationState.Registered &&
          _obj.DocumentKind.NumberingType != Sungero.Docflow.DocumentKind.NumberingType.Numerable ||
          _obj.RegistrationState == SuppliesContract.RegistrationState.Reserved)
      {
        // Используем диалоги, чтобы хинт не пробрасывался в задачу, в которую он вложен.
        Dialogs.ShowMessage(Sungero.Docflow.OfficialDocuments.Resources.NeedCancelRegistration, MessageType.Error);
        return;
      }

      var types = centrvd.StudySolution.PublicFunctions.OfficialDocument.GetTypesAvailableForChange(_obj);
      var convertedDocument = centrvd.StudySolution.PublicFunctions.OfficialDocument.ChangeDocumentType(_obj,types);
      if (convertedDocument != null)
      {
        // Dmitriev_IA: Критически важно для корректного открытия в десктоп клиенте карточки сконвертированного документа.
        e.CloseFormAfterAction = true;
        convertedDocument.Show();
      }
    }

    public override bool CanChangeDocumentType(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return  !_obj.State.IsInserted && !_obj.State.IsChanged && _obj.AccessRights.CanUpdate() && _obj.InternalApprovalState == null;
    }

    public override void SendForFreeApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.SendForFreeApproval(e);
    }

    public override bool CanSendForFreeApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      var canSend = base.CanSendForFreeApproval(e);
      return canSend && !PublicFunctions.SuppliesContract.Remote.IsSigned(_obj);
    }

    public override void SendForApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.SendForApproval(e);
    }

    public override bool CanSendForApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      var canSend =  base.CanSendForApproval(e);
      return canSend && !PublicFunctions.SuppliesContract.Remote.IsSigned(_obj);
    }

    public override void ScanInNewVersion(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.ScanInNewVersion(e);
    }

    public override bool CanScanInNewVersion(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return !PublicFunctions.SuppliesContract.Remote.OnApproval(_obj);
    }

    public override void CreateFromScanner(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.CreateFromScanner(e);
    }

    public override bool CanCreateFromScanner(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return !PublicFunctions.SuppliesContract.Remote.OnApproval(_obj);
    }

    public override void CreateFromFile(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.CreateFromFile(e);
    }

    public override bool CanCreateFromFile(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return !PublicFunctions.SuppliesContract.Remote.OnApproval(_obj);
      
    }

  }

}