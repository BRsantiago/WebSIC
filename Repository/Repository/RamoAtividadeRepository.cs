﻿using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RamoAtividadeRepository : RepositoryBase<RamoAtividade>, IRamoAtividadeRepository
    {
        public RamoAtividadeRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }
    }
}