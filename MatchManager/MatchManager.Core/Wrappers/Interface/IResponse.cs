﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Core.Wrappers.Interface
{
    public interface IResponse : ICoreResult
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
