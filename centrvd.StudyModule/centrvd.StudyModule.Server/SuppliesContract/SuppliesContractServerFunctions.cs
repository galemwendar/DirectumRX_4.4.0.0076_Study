using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using centrvd.StudyModule.SuppliesContract;

namespace centrvd.StudyModule.Server
{
  partial class SuppliesContractFunctions
  {

    /// <summary>
    /// <returns>true, если документ на согласовании</returns>
    /// </summary>
    [Public,Remote]
    public bool OnApproval()
    {
      return Equals(_obj.InternalApprovalState, InternalApprovalState.OnApproval) ||
        Equals(_obj.InternalApprovalState, InternalApprovalState.OnApproval) ||
        Equals(_obj.InternalApprovalState, InternalApprovalState.OnRework) ||
        Equals(_obj.InternalApprovalState, InternalApprovalState.PendingSign);
    }
    
    /// <summary>
    /// <returns>true, если документ подписан</returns>
    /// </summary>
    [Public,Remote]
    public bool IsSigned()
    {
      return Equals(_obj.InternalApprovalState, InternalApprovalState.Signed);
    }
  }
}