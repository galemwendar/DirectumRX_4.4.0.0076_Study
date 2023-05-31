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
       CreateDocumentKinds();
    }
    
    
    public void CreateDocumentKinds()
    {
      InitializationLogger.Debug("StudyModule_Init: Create document kinds.");
      
      var actions = new Sungero.Domain.Shared.IActionInfo[] {
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendActionItem,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForFreeApproval,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForApproval,
        Sungero.Docflow.OfficialDocuments.Info.Actions.SendForAcquaintance };
      var numerable = Sungero.Docflow.DocumentKind.NumberingType.Numerable;

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
  }
}
