using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.DocumentRegister;

namespace centrvd.StudySolution
{
  partial class DocumentRegisterNumberFormatItemsClientHandlers
  {

    public override IEnumerable<Enumeration> NumberFormatItemsElementFiltering(IEnumerable<Enumeration> query)
    {
      query = base.NumberFormatItemsElementFiltering(query);
      if(_obj.DocumentRegister.DocumentFlow != DocumentRegister.DocumentFlow.Inner)
        return query.Where(x => x.Value != DocumentRegisterNumberFormatItems.Element.BUId.Value);
      return query;
    }
  }

  partial class DocumentRegisterClientHandlers
  {

  }
}