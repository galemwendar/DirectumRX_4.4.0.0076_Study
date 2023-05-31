using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.DocumentKind;

namespace centrvd.StudySolution.Client
{
  partial class DocumentKindCollectionActions
  {
    public override void SetAccessRights(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      base.SetAccessRights(e);
    }

    public override bool CanSetAccessRights(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return base.CanSetAccessRights(e);
    }

  }

}