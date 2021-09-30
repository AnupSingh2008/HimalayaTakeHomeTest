﻿using Himalaya.Data.Contexts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalaya.Data.Interfaces
{
    public interface IOrderBookDbContextFactory
    {
        OrderBookDbContext Create();
        SqlConnection Connection();
    }
}