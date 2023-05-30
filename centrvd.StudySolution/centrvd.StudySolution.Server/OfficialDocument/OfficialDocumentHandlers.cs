using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.OfficialDocument;

namespace centrvd.StudySolution
{
  partial class OfficialDocumentDocumentKindSearchPropertyFilteringServerHandler<T>
  {

    public override IQueryable<T> DocumentKindSearchDialogFiltering(IQueryable<T> query, Sungero.Domain.PropertySearchDialogFilteringEventArgs e)
    {

      query = base.DocumentKindSearchDialogFiltering(query, e);
      return query;
    }
  }

  partial class OfficialDocumentDocumentKindPropertyFilteringServerHandler<T>
  {

    public override IQueryable<T> DocumentKindFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      //TODO Да еперный театр, я не хочу просто возвращать нуль если нет НОР
      var list = new List<IRecipient>();
      foreach (var element in query)
      {
        list.AddRange(element.AccessRights.GetSubstitutedWhoCanSelectInDocument());
      }
      query = base.DocumentKindFiltering(query, e);
      return query;
    }
  }



}