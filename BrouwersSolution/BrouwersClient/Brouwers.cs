﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BrouwersClient
{
    [CollectionDataContract(Name ="brouwers", Namespace ="")]
    public class Brouwers : List<BrouwerBeknopt>
    {
    }
}