using MatchManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Common
{
    public static class UserUtility
    {
        public static AuditableBaseEntity SetAuditabilityDateTime(AuditableBaseEntity model)
        {
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            return model;
        }
    }
}
