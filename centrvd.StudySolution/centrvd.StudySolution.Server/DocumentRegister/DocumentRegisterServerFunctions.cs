using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudySolution.DocumentRegister;

namespace centrvd.StudySolution.Server
{
  partial class DocumentRegisterFunctions
  {
    public override string GetNextNumber(DateTime date, int leadDocumentId, Sungero.Docflow.IOfficialDocument document, string leadingDocumentNumber, int departmentId, int businessUnitId, string caseFileIndex, string docKindCode, string indexLeadingSymbol)
    {
      //добавить ID НОР
      return base.GetNextNumber(date, leadDocumentId, document, leadingDocumentNumber, departmentId, businessUnitId, caseFileIndex, docKindCode, indexLeadingSymbol);
    }
    
  }
}