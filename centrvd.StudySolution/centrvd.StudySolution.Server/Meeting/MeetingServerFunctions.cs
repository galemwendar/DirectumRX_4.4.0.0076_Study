using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.Meeting;

namespace centrvd.StudySolution.Server
{
  partial class MeetingFunctions
  {

    /// <summary>
    /// Отправить уведомления об изменении совещания
    /// <param name="text">текст уведомления</param>
    /// </summary>
    [Public]
    public void SendNotificationBySimpleTask(string text)
    {
      var attachment = new IMeeting[] {_obj};
      var recipients = _obj.Members.Select( c => c.Member).ToArray();
      var task = Sungero.Workflow.SimpleTasks.CreateWithNotices(centrvd.StudySolution.Meetings.Resources.MeetingConditionsChangedFormat(_obj.Name), recipients,attachment);
      task.ActiveText = text;
      task.Save();
      task.Start();
    }

  }
}