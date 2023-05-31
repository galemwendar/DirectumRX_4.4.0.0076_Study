using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace centrvd.StudyModule.Server
{
  public partial class ModuleInitializer
  {
    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      // Выдача прав всем пользователям.
      var allUsers = Roles.AllUsers;
      CreateDocumentTypes();
      CreateDocumentKinds();
      GrantRightsOnDocuments(allUsers);
    }
    
    
    public void CreateDocumentKinds()
    {
      InitializationLogger.Debug("StudyModule_Init: Create document kinds.");
      
      var actions = new Sungero.Domain.Shared.IActionInfo[] {
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendActionItem,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForFreeApproval,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForApproval,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForAcquaintance };
      var registrable = Sungero.Docflow.DocumentKind.NumberingType.Registrable;
var numerable = Sungero.Docflow.DocumentKind.NumberingType.Numerable;
      //Договор размещения ресурсов.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Constants.Module.DocumentKindNames.SuppliesAllocationContract,
                                                                              Constants.Module.DocumentKindNames.SuppliesAllocationContractShort,
                                                                              registrable,
                                                                              Sungero.Docflow.DocumentKind.DocumentFlow.Contracts,
                                                                              true,
                                                                              true,
                                                                              SuppliesAllocationContract.ClassTypeGuid,
                                                                              actions,
                                                                              Constants.Module.DocumentKindGuid.SuppliesAllocationContract,true);
      //Договор передачи ресурсов в аренду.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Constants.Module.DocumentKindNames.SuppliesLeaseContract,
                                                                              Constants.Module.DocumentKindNames.SuppliesLeaseContractShort,
                                                                              registrable,
                                                                              Sungero.Docflow.DocumentKind.DocumentFlow.Contracts,
                                                                              true,
                                                                              true,
                                                                              SuppliesLeaseContract.ClassTypeGuid,
                                                                              actions,
                                                                              Constants.Module.DocumentKindGuid.SuppliesLeaseContract,true);
    
      //Служебная записка на предоставление доступа.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Constants.Module.DocumentKindNames.GrantingAcessMemo,
                                                                              Constants.Module.DocumentKindNames.GrantingAcessMemoShort,
                                                                              numerable,
                                                                              Sungero.Docflow.DocumentKind.DocumentFlow.Inner,
                                                                              false,
                                                                              false,
                                                                              Sungero.Docflow.Server.Memo.ClassTypeGuid,
                                                                              actions,
                                                                              Constants.Module.DocumentKindGuid.GrantingAcessMemo,false);
      
      //Служебная записка о проведении совещания.
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentKind(Constants.Module.DocumentKindNames.MeetingMemo,
                                                                              Constants.Module.DocumentKindNames.MeetingMemoShort,
                                                                              numerable,
                                                                              Sungero.Docflow.DocumentKind.DocumentFlow.Inner,
                                                                              false,
                                                                              false,
                                                                              Sungero.Docflow.Server.Memo.ClassTypeGuid,
                                                                              actions,
                                                                              Constants.Module.DocumentKindGuid.MeetingMemo,false);
      
    }
    
    public static void CreateDocumentTypes()
    {
      InitializationLogger.Debug("StudyModule_Init: Create document types");
      
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType(Constants.Module.DocumentKindNames.SuppliesAllocationContract,
                                                                              SuppliesAllocationContract.ClassTypeGuid,
                                                                              Sungero.Docflow.DocumentType.DocumentFlow.Contracts,
                                                                              true);
      Sungero.Docflow.PublicInitializationFunctions.Module.CreateDocumentType(Constants.Module.DocumentKindNames.SuppliesLeaseContract,
                                                                              SuppliesLeaseContract.ClassTypeGuid,
                                                                               Sungero.Docflow.DocumentType.DocumentFlow.Contracts,
                                                                              true);
    }
    
    public static void GrantRightsOnDocuments(IRole allUsers)
    {
      InitializationLogger.Debug("StudyModule_Init: Grant rights on documents to all users.");
      
      SuppliesAllocationContracts.AccessRights.Grant(allUsers, DefaultAccessRightsTypes.Create);
      SuppliesAllocationContracts.AccessRights.Save();
      
      SuppliesLeaseContracts.AccessRights.Grant(allUsers, DefaultAccessRightsTypes.Create);
      SuppliesLeaseContracts.AccessRights.Save();
    }
    
  }
}
