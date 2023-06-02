using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.Meeting;

namespace centrvd.StudySolution
{
  partial class MeetingServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      var properties = new List<Sungero.Domain.Shared.IPropertyState>() {_obj.State.Properties.Location, _obj.State.Properties.DateTime};
      var propertiesType = Meetings.Info.Properties.GetType();
      var objType = _obj.GetType();
      foreach (var property in properties)
      {
        var isChanged = (property as Sungero.Domain.Shared.IPropertyState).IsChanged;
        if(isChanged)
        {
          // Извлечение локализованного наименования реквизита.
          var propertyName = (property as Sungero.Domain.Shared.PropertyStateBase).PropertyName;
          var propertyInfo = propertiesType.GetProperty(propertyName).GetValue(Meetings.Info.Properties);
          var name = propertyInfo.GetType().GetProperty("LocalizedName").GetValue(propertyInfo);
          
          // Извлечение нового значения реквизита.
          var newValue = objType.GetProperty(propertyName).GetValue(_obj);
          
          // Извлечение старого значения реквизита.
          var oldValue = property.GetType().GetProperty("OriginalValue").GetValue(property);
          
          // Дополнительно еще раз сверяет что старое и новое значения не равны, также отбрасываем изменения строковых свойств с null на пустую строку
          if (newValue == oldValue ||
              newValue != null && oldValue != null && Equals(newValue.ToString(), oldValue.ToString()) ||
              newValue != null && oldValue == null && string.IsNullOrEmpty(newValue.ToString()) ||
              oldValue != null && newValue == null && string.IsNullOrEmpty(oldValue.ToString()))
            continue;
          var text = centrvd.StudySolution.Meetings.Resources.MeetingConditionsHaveChangedActiveTextFormat( name, newValue, oldValue);
          Functions.Meeting.SendNotificationBySimpleTask(_obj, text);
        }
      }
    }
  }

}