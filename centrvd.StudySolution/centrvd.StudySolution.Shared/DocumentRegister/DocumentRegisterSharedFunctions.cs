using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.DocumentRegister;

namespace centrvd.StudySolution.Shared
{
  partial class DocumentRegisterFunctions
  {
    
    /// <summary>
    /// Генерировать префикс и постфикс регистрационного номера документа.
    /// </summary>
    /// <param name="date">Дата.</param>
    /// <param name="leadingDocumentNumber">Ведущий документ.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="counterpartyCodeIsMetasymbol">Признак того, что код контрагента нужен в виде метасимвола.</param>
    /// <returns>Сгенерированный регистрационный номер.</returns>
    public override Sungero.Docflow.Structures.DocumentRegister.RegistrationNumberParts GenerateRegistrationNumberPrefixAndPostfix(DateTime date,
                                                                                                                                   string leadingDocumentNumber,
                                                                                                                                   string departmentCode,
                                                                                                                                   string businessUnitCode,
                                                                                                                                   string caseFileIndex,
                                                                                                                                   string docKindCode,
                                                                                                                                   string counterpartyCode,
                                                                                                                                   bool counterpartyCodeIsMetasymbol)
    {
      var prefix = string.Empty;
      var postfix = string.Empty;
      var numberElement = string.Empty;
      var orderedNumberFormatItems = _obj.NumberFormatItems.OrderBy(f => f.Number);
      foreach (var element in orderedNumberFormatItems)
      {
        if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Number)
        {
          prefix = numberElement;
          numberElement = string.Empty;
        }
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Log)
          numberElement += _obj.Index;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.RegistrPlace && _obj.RegistrationGroup != null)
          numberElement += _obj.RegistrationGroup.Index;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Year2Place)
          numberElement += date.ToString("yy");
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Year4Place)
          numberElement += date.ToString("yyyy");
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Month)
          numberElement += date.ToString("MM");
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Quarter)
          numberElement += ToQuarterString(date);
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.Day)
          numberElement += date.ToString("dd");
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.LeadingNumber)
          numberElement += leadingDocumentNumber;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.DepartmentCode)
          numberElement += departmentCode;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.BUCode)
          numberElement += businessUnitCode;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.CaseFile)
          numberElement += caseFileIndex;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.DocKindCode)
          numberElement += docKindCode;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode && !counterpartyCodeIsMetasymbol)
          numberElement += counterpartyCode;
        else if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode && counterpartyCodeIsMetasymbol)
          numberElement += Sungero.Docflow.DocumentRegisters.Resources.NumberFormatCounterpartyCode;
        else if (element.Element == DocumentRegisterNumberFormatItems.Element.BUId && Sungero.Company.Employees.Current?.Department?.BusinessUnit != null)
          numberElement += Sungero.Company.Employees.Current?.Department?.BusinessUnit?.Id.ToString();
        
        // Не добавлять разделитель, для пустого кода контрагента.
        if (string.IsNullOrEmpty(counterpartyCode) || counterpartyCodeIsMetasymbol)
        {
          // Разделитель после пустого кода контрагента.
          if (element.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode)
            continue;
          
          // Разделитель до кода контрагента, если код контрагента последний в номере.
          var nextElement = orderedNumberFormatItems.Where(f => f.Number > element.Number).FirstOrDefault();
          var lastElement = orderedNumberFormatItems.LastOrDefault();
          if (nextElement != null && nextElement.Element == Sungero.Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode &&
              lastElement != null && lastElement.Number == nextElement.Number)
            continue;
        }
        
        // Добавить разделитель.
        numberElement += element.Separator;
      }
      
      postfix = numberElement;
      return Sungero.Docflow.Structures.DocumentRegister.RegistrationNumberParts.Create(prefix, postfix);
    }
  }
}