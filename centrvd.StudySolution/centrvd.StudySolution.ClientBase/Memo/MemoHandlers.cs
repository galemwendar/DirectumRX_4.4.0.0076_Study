using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.Memo;

namespace centrvd.StudySolution
{
  partial class MemoClientHandlers
  {

    public override void DocumentKindValueInput(Sungero.Docflow.Client.OfficialDocumentDocumentKindValueInputEventArgs e)
    {
      var properties = _obj.State.Properties;
      var propertiesGrantingAcess = new List<Sungero.Domain.Shared.IPropertyState>(){properties.Whomcentrvd, properties.Wherecentrvd, properties.Accesstypecentrvd};
      var propertiesMeeting = new List<Sungero.Domain.Shared.IPropertyState>(){properties.Venuecentrvd,properties.Participantscentrvd};
      
      base.DocumentKindValueInput(e);
      if( e.NewValue != e.OldValue && e.NewValue != null)
      {
        var isGrantingAcessMemo = e.NewValue.Name == StudyModule.PublicConstants.Module.DocumentKindNames.GrantingAcessMemo;
        var isMeetingMemo = e.NewValue.Name == StudyModule.PublicConstants.Module.DocumentKindNames.MeetingMemo;
        foreach (var prop in propertiesGrantingAcess)
        {
          prop.IsVisible = isGrantingAcessMemo;
          prop.IsEnabled = isGrantingAcessMemo;
        }
        
        foreach (var prop in propertiesMeeting)
        {
          prop.IsVisible = isMeetingMemo;
          prop.IsEnabled = isMeetingMemo;
        }

      }
    }

  }
}