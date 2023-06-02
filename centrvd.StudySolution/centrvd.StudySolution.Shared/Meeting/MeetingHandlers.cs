using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.Meeting;

namespace centrvd.StudySolution
{
  partial class MeetingSharedHandlers
  {

    public override void LocationChanged(Sungero.Domain.Shared.StringPropertyChangedEventArgs e)
    {
      base.LocationChanged(e);
    }

    public override void DateTimeChanged(Sungero.Domain.Shared.DateTimePropertyChangedEventArgs e)
    {
      base.DateTimeChanged(e);
    }

  }
}