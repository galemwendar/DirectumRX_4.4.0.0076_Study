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
    #region Работа с номером документа
    /// <summary>
    /// Генерировать регистрационный номер для документа.
    /// </summary>
    /// <param name="date">Дата регистрации.</param>
    /// <param name="index">Номер.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="leadingDocumentNumber">Номер ведущего документа.</param>
    /// <param name="businessUnitCode">ИД нашей организации.</param>
    /// <returns>Сгенерированный регистрационный номер.</returns>
    [Public]
    public override string GenerateRegistrationNumber(DateTime date, string index, string departmentCode, string businessUnitCode,
                                                     string caseFileIndex, string docKindCode, string counterpartyCode, string leadingDocumentNumber, string businessUnitId)
    {
      return Functions.DocumentRegister.GenerateRegistrationNumber(_obj, date, index, leadingDocumentNumber, departmentCode, businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, "0", businessUnitId);
    }

    /// <summary>
    /// Генерировать регистрационный номер для диалога регистрации.
    /// </summary>
    /// <param name="date">Дата регистрации.</param>
    /// <param name="index">Номер.</param>
    /// <param name="leadingDocumentNumber">Номер ведущего документа.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="indexLeadingSymbol">Символ для заполнения ведущих значений индекса в номере.</param>
    /// <param name="businessUnitCode">ИД нашей организации.</param>
    /// <returns>Сгенерированный регистрационный номер.</returns>
    public override string GenerateRegistrationNumberFromDialog(DateTime date, string index, string leadingDocumentNumber,
                                                               string departmentCode, string businessUnitCode, string caseFileIndex,
                                                               string docKindCode, string counterpartyCode, string indexLeadingSymbol = "0", string businessUnitId)
    {
      return Functions.DocumentRegister.GenerateRegistrationNumber(_obj, date, index, leadingDocumentNumber, departmentCode, businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, indexLeadingSymbol, businessUnitId);
    }
    
    /// <summary>
    /// Генерировать регистрационный номер для документа.
    /// </summary>
    /// <param name="date">Дата регистрации.</param>
    /// <param name="index">Номер.</param>
    /// <param name="leadingDocumentNumber">Номер ведущего документа.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="indexLeadingSymbol">Символ для заполнения ведущих значений индекса в номере.</param>
    /// <param name="businessUnitCode">ИД нашей организации.</param>
    /// <returns>Сгенерированный регистрационный номер.</returns>
    [Public]
    public override string GenerateRegistrationNumber(DateTime date, string index, string leadingDocumentNumber,
                                                     string departmentCode, string businessUnitCode, string caseFileIndex,
                                                     string docKindCode, string counterpartyCode, string indexLeadingSymbol, string businessUnitId)
    {
      // Сформировать регистрационный индекс.
      var registrationNumber = string.Empty;
      if (index.Length < _obj.NumberOfDigitsInNumber)
        registrationNumber = string.Concat(Enumerable.Repeat(indexLeadingSymbol, (_obj.NumberOfDigitsInNumber - index.Length) ?? 0));
      registrationNumber += index;
      
      // Сформировать регистрационный номер.
      var prefixAndPostfix = Functions.DocumentRegister.GenerateRegistrationNumberPrefixAndPostfix(_obj, date, leadingDocumentNumber, departmentCode,
                                                                                                   businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, businessUnitId, false);
      return string.Format("{0}{1}{2}", prefixAndPostfix.Prefix, registrationNumber, prefixAndPostfix.Postfix);
    }
    
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
    /// <param name="businessUnitId">ИД нашей организации.</param>
    /// <param name="counterpartyCodeIsMetasymbol">Признак того, что код контрагента нужен в виде метасимвола.</param>
    /// <returns>Сгенерированный регистрационный номер.</returns>
    public override Sungero.Docflow.Structures.DocumentRegister.RegistrationNumberParts GenerateRegistrationNumberPrefixAndPostfix(DateTime date,
                                                                                                                                   string leadingDocumentNumber,
                                                                                                                                   string departmentCode,
                                                                                                                                   string businessUnitCode,
                                                                                                                                   string caseFileIndex,
                                                                                                                                   string docKindCode,
                                                                                                                                   string counterpartyCode,
                                                                                                                                   string businessUnitId,
                                                                                                                                   bool counterpartyCodeIsMetasymbol)
    {
      var prefix = string.Empty;
      var postfix = string.Empty;
      var numberElement = string.Empty;
      var orderedNumberFormatItems = _obj.NumberFormatItems.OrderBy(f => f.Number);
      foreach (var element in orderedNumberFormatItems)
      {
        if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Number)
        {
          prefix = numberElement;
          numberElement = string.Empty;
        }
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Log)
          numberElement += _obj.Index;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.RegistrPlace && _obj.RegistrationGroup != null)
          numberElement += _obj.RegistrationGroup.Index;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Year2Place)
          numberElement += date.ToString("yy");
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Year4Place)
          numberElement += date.ToString("yyyy");
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Month)
          numberElement += date.ToString("MM");
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Quarter)
          numberElement += ToQuarterString(date);
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.Day)
          numberElement += date.ToString("dd");
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.LeadingNumber)
          numberElement += leadingDocumentNumber;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.DepartmentCode)
          numberElement += departmentCode;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.BUCode)
          numberElement += businessUnitCode;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.CaseFile)
          numberElement += caseFileIndex;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.DocKindCode)
          numberElement += docKindCode;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode && !counterpartyCodeIsMetasymbol)
          numberElement += counterpartyCode;
        else if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode && counterpartyCodeIsMetasymbol)
          numberElement += DocumentRegisters.Resources.NumberFormatCounterpartyCode;
        else if (element.Element == DocumentRegisterNumberFormatItems.Element.BUId)
          numberElement += businessUnitId;
        
        // Не добавлять разделитель, для пустого кода контрагента.
        if (string.IsNullOrEmpty(counterpartyCode) || counterpartyCodeIsMetasymbol)
        {
          // Разделитель после пустого кода контрагента.
          if (element.Element == Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode)
            continue;
          
          // Разделитель до кода контрагента, если код контрагента последний в номере.
          var nextElement = orderedNumberFormatItems.Where(f => f.Number > element.Number).FirstOrDefault();
          var lastElement = orderedNumberFormatItems.LastOrDefault();
          if (nextElement != null && nextElement.Element == Docflow.DocumentRegisterNumberFormatItems.Element.CPartyCode &&
              lastElement != null && lastElement.Number == nextElement.Number)
            continue;
        }
        
        // Добавить разделитель.
        numberElement += element.Separator;
      }
      
      postfix = numberElement;
      return Structures.DocumentRegister.RegistrationNumberParts.Create(prefix, postfix);
    }

    
    /// <summary>
    /// Получить формат номера журнала регистрации для отчета.
    /// </summary>
    /// <returns>Формат номера для отчета.</returns>
    /// <remarks>Используется в SkippedNumbersReport.</remarks>
    public override string GetReportNumberFormat()
    {
      var numberFormat = string.Empty;

      foreach (var item in _obj.NumberFormatItems.OrderBy(x => x.Number))
      {
        var elementName = string.Empty;
        if (item.Element == DocumentRegisterNumberFormatItems.Element.Number)
          elementName = DocumentRegisters.Resources.NumberFormatNumber;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.Year2Place || item.Element == DocumentRegisterNumberFormatItems.Element.Year4Place)
          elementName = DocumentRegisters.Resources.NumberFormatYear;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.Quarter)
          elementName = DocumentRegisters.Resources.NumberFormatQuarter;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.Month)
          elementName = DocumentRegisters.Resources.NumberFormatMonth;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.Day)
          elementName = DocumentRegisters.Resources.NumberFormatDay;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.LeadingNumber)
          elementName = DocumentRegisters.Resources.NumberFormatLeadingNumber;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.Log)
          elementName = DocumentRegisters.Resources.NumberFormatLog;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.RegistrPlace)
          elementName = DocumentRegisters.Resources.NumberFormatRegistrPlace;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.CaseFile)
          elementName = DocumentRegisters.Resources.NumberFormatCaseFile;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.DepartmentCode)
          elementName = DocumentRegisters.Resources.NumberFormatDepartmentCode;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.BUCode)
          elementName = DocumentRegisters.Resources.NumberFormatBUCode;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.DocKindCode)
          elementName = DocumentRegisters.Resources.NumberFormatDocKindCode;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.CPartyCode)
          elementName = DocumentRegisters.Resources.NumberFormatCounterpartyCode;
        else if (item.Element == DocumentRegisterNumberFormatItems.Element.BUId)
          elementName = centrvd.StudySolution.DocumentRegisters.Resources.NumberFormatBusinessUnitId;

        numberFormat += elementName + item.Separator;
      }

      return numberFormat;
    }
    
    /// <summary>
    /// Проверить регистрационный номер на валидность.
    /// </summary>
    /// <param name="registrationDate">Дата регистрации.</param>
    /// <param name="registrationNumber">Номер регистрации.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="leadDocNumber">Номер ведущего документа.</param>
    /// <param name="searchCorrectingPostfix">Искать корректировочный постфикс.</param>
    /// <param name="businessUnitid">ИД нашей организации.</param>
    /// <returns>Сообщение об ошибке. Пустая строка, если номер соответствует журналу.</returns>
    /// <remarks>Пример: 5/1-П/2020, где 5 - порядковый номер, П - индекс журнала, 2020 - год, /1 - корректировочный постфикс.</remarks>
    public override string CheckRegistrationNumberFormat(DateTime? registrationDate, string registrationNumber,
                                                        string departmentCode, string businessUnitCode, string caseFileIndex, string docKindCode, string counterpartyCode,
                                                        string leadDocNumber, bool searchCorrectingPostfix, string businessUnitid)
    {
      if (string.IsNullOrWhiteSpace(registrationNumber))
        return DocumentRegisters.Resources.EnterRegistrationNumber;
      
      // Регулярное выражение для рег. индекса.
      // "([0-9]+)" определяет, где искать индекс в номере.
      // "([\.\/-][0-9]+)?" определяет, где искать корректировочный постфикс в номере.
      // Пустые скобки в выражении @"([0-9]+)()" означают корректировочный постфикс,
      // чтобы количество групп в результате регулярного выражения было всегда одинаковым, независимо от того, нужно искать корректировочный постфикс или нет.
      var indexTemplate = searchCorrectingPostfix ? @"([0-9]+)([\.\/-][0-9]+)?" : @"([0-9]+)()";
      
      // Перед проверкой правильности формата дополнительно проверить наличие непечатных символов в строке ("\s").
      if (Regex.IsMatch(registrationNumber, @"\s"))
        return DocumentRegisters.Resources.NoSpaces;
      
      if (!GetRegexMatchFromRegistrationNumber(_obj, registrationDate ?? Calendar.UserToday, registrationNumber, indexTemplate,
                                               departmentCode, businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, leadDocNumber,
                                               string.Empty, string.Empty, businessUnitid)
          .Success)
      {
        // Шаблон номера, состоящий из символов "*".
        var numberTemplate = string.Concat(Enumerable.Repeat("*", _obj.NumberOfDigitsInNumber.Value));
        var example = this.GenerateRegistrationNumber(registrationDate.Value, numberTemplate, departmentCode, businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, "0", businessUnitid);
        return Docflow.Resources.RegistrationNumberNotMatchFormatFormat(example);
      }
      
      return string.Empty;
    }

    /// <summary>
    /// Получить сравнение рег.номера с шаблоном.
    /// </summary>
    /// <param name="documentRegister">Журнал.</param>
    /// <param name="date">Дата.</param>
    /// <param name="registrationNumber">Рег. номер.</param>
    /// <param name="indexTemplate">Шаблон номера.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="leadDocNumber">Номер ведущего документа.</param>
    /// <param name="numberPostfix">Постфикс номера.</param>
    /// <param name="additionalPrefix">Дополнительный префикс номера.</param>
    /// <param name="businessUnitid">ИД нашей организации.</param>
    /// <returns>Индекс.</returns>
    internal static Match GetRegexMatchFromRegistrationNumber(IDocumentRegister documentRegister, DateTime date, string registrationNumber,
                                                              string indexTemplate, string departmentCode, string businessUnitCode,
                                                              string caseFileIndex, string docKindCode, string counterpartyCode, string leadDocNumber,
                                                              string numberPostfix, string additionalPrefix, string businessUnitid)
    {
      var prefixAndPostfix = Functions.DocumentRegister.GenerateRegistrationNumberPrefixAndPostfix(documentRegister, date, leadDocNumber, departmentCode,
                                                                                                   businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, businessUnitid, true);
      var template = string.Format("{0}{1}{2}{3}", Regex.Escape(prefixAndPostfix.Prefix),
                                   indexTemplate,
                                   Regex.Escape(prefixAndPostfix.Postfix),
                                   numberPostfix);
      
      // Заменить метасимвол для кода контрагента на соответствующее регулярное выражение.
      var metaCounterpartyCode = Regex.Escape(DocumentRegisters.Resources.NumberFormatCounterpartyCode);
      template = template.Replace(metaCounterpartyCode, Constants.DocumentRegister.CounterpartyCodeRegex);
      
      // Совпадение в начале строки.
      var numberTemplate = string.Format("^{0}", template);
      var match = Regex.Match(registrationNumber, numberTemplate);
      if (match.Success)
        return match;
      
      // Совпадение в конце строки.
      numberTemplate = string.Format("{0}{1}$", additionalPrefix, template);
      return Regex.Match(registrationNumber, numberTemplate);
    }

    /// <summary>
    /// Получить индекс рег. номера.
    /// </summary>
    /// <param name="documentRegister">Журнал.</param>
    /// <param name="date">Дата.</param>
    /// <param name="registrationNumber">Рег. номер.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="leadDocNumber">Номер ведущего документа.</param>
    /// <param name="searchCorrectingPostfix">Искать корректировочный постфикс.</param>
    /// <param name="businessUnitid">ИД нашей организации.</param>
    /// <returns>Индекс.</returns>
    /// <remarks>Пример: 5/1-П/2020, где 5 - порядковый номер, П - индекс журнала, 2020 - год, /1 - корректировочный постфикс.</remarks>
    public static int GetIndexFromRegistrationNumber(IDocumentRegister documentRegister, DateTime date, string registrationNumber,
                                                     string departmentCode, string businessUnitCode, string caseFileIndex, string docKindCode, string counterpartyCode,
                                                     string leadDocNumber, bool searchCorrectingPostfix, string businessUnitid)
    {
      return ParseRegistrationNumber(documentRegister, date, registrationNumber, departmentCode, businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, leadDocNumber, searchCorrectingPostfix, businessUnitid).Index;
    }
    
    /// <summary>
    /// Выделить составные части рег.номера.
    /// </summary>
    /// <param name="documentRegister">Журнал.</param>
    /// <param name="date">Дата.</param>
    /// <param name="registrationNumber">Рег. номер.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="leadDocNumber">Номер ведущего документа.</param>
    /// <param name="searchCorrectingPostfix">Искать корректировочный постфикс.</param>
    /// <param name="businessUnitid">ИД нашей организации.</param>
    /// <returns>Индекс рег.номера.</returns>
    /// <remarks>Пример: 5/1-П/2020, где 5 - порядковый номер, П - индекс журнала, 2020 - год, /1 - корректировочный постфикс.</remarks>
    public static Structures.DocumentRegister.RegistrationNumberIndex ParseRegistrationNumber(IDocumentRegister documentRegister, DateTime date, string registrationNumber,
                                                                                              string departmentCode, string businessUnitCode, string caseFileIndex, string docKindCode, string counterpartyCode,
                                                                                              string leadDocNumber, bool searchCorrectingPostfix, string businessUnitid)
    {
      // "(.*?)" определяет место, в котором находятся искомые данные.
      var releasingExpression = "(.*?)$";
      // Регулярное выражение для рег. индекса.
      // "([0-9]+)" определяет, где искать индекс в номере.
      // "([\.\/-][0-9]+)?" определяет, где искать корректировочный постфикс в номере.
      // Пустые скобки в выражении @"([0-9]+)()" означают корректировочный постфикс,
      // чтобы количество групп в результате регулярного выражения было всегда одинаковым, независимо от того, нужно искать корректировочный постфикс или нет.
      var indexExpression = searchCorrectingPostfix ? @"([0-9]+)([\.\/-][0-9]+)?" : @"([0-9]+)()";
      
      // Распарсить рег.номер на составляющие.
      var registrationNumberMatch = GetRegexMatchFromRegistrationNumber(documentRegister, date, registrationNumber, indexExpression,
                                                                        departmentCode, businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, leadDocNumber,
                                                                        releasingExpression, string.Empty, businessUnitid);
      var indexOfNumber = registrationNumberMatch.Groups[1].Value;
      var correctingPostfix = registrationNumberMatch.Groups[2].Value;
      var postfix = registrationNumberMatch.Groups[3].Value;
      
      int index;
      index = int.TryParse(indexOfNumber, out index) ? index : 0;
      return Structures.DocumentRegister.RegistrationNumberIndex.Create(index, postfix, correctingPostfix);
    }
    
    /// <summary>
    /// Проверить совпадение рег.номеров.
    /// </summary>
    /// <param name="documentRegister">Журнал.</param>
    /// <param name="date">Дата.</param>
    /// <param name="registrationNumber">Рег. номер.</param>
    /// <param name="departmentCode">Код подразделения.</param>
    /// <param name="businessUnitCode">Код нашей организации.</param>
    /// <param name="caseFileIndex">Индекс дела.</param>
    /// <param name="docKindCode">Код вида документа.</param>
    /// <param name="counterpartyCode">Код контрагента.</param>
    /// <param name="leadDocNumber">Номер ведущего документа.</param>
    /// <param name="registrationNumberSample">Пример рег. номера.</param>
    /// <param name="searchCorrectingPostfix">Искать корректировочный постфикс.</param>
    /// <param name="businessUnitid">ИД нашей организации.</param>
    /// <returns>True, если совпадают.</returns>
    /// <remarks>Пример: 5/1-П/2020, где 5 - порядковый номер, П - индекс журнала, 2020 - год, /1 - корректировочный постфикс.</remarks>
    public static bool IsEqualsRegistrationNumbers(IDocumentRegister documentRegister, DateTime date, string registrationNumber,
                                                   string departmentCode, string businessUnitCode, string caseFileIndex, string docKindCode, string counterpartyCode,
                                                   string leadDocNumber, string registrationNumberSample,
                                                   bool searchCorrectingPostfix, string businessUnitId)
    {
      var indexAndPostfix = ParseRegistrationNumber(documentRegister, date, registrationNumberSample, departmentCode, businessUnitCode,
                                                    caseFileIndex, docKindCode, counterpartyCode, leadDocNumber, searchCorrectingPostfix, businessUnitId);
      var maxLeadZeroIndexCount = 9 - indexAndPostfix.Index.ToString().Count();
      var indexRegexTemplate = "[0]{0," + maxLeadZeroIndexCount + "}" + indexAndPostfix.Index + Regex.Escape(indexAndPostfix.CorrectingPostfix);
      
      return GetRegexMatchFromRegistrationNumber(documentRegister, date, registrationNumber, indexRegexTemplate, departmentCode,
                                                 businessUnitCode, caseFileIndex, docKindCode, counterpartyCode, leadDocNumber,
                                                 indexAndPostfix.Postfix + "$", "^").Success;
    }
    #endregion
    
    
  }
}